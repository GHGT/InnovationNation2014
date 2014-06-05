using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DroneSimulator
{
    public partial class Form1 : Form
    {
        struct coordinate
        {
            public double x;
            public double y;
        };

        struct INSTRUCTION
        {
            public double x;
            public double y;
            public int timer;
            public int led;
        };

        //center point
        int center_x = 200;
        int center_y = 200;
        //boundary
        const double ulx = 0;
        const double uly = 0;
        const double brx = 960;
        const double bry = 680;

        const Boolean debug = false;
        
        //Random variant
        Boolean environment_random;
        double random_x = 0;
        double random_y = 0;
        Random r;
        double random_rate = 2;

        //delayed x n y rate
        double r_x = 0.05;
        double r_y = 0.2;

        //cpu counter
        Boolean start;
        //led status
        Boolean led;

        //general variables
        private int i;
        private System.Drawing.Graphics g;
        private System.Drawing.Brush plane;
        private System.Drawing.Brush plane_led;
        private System.Drawing.Brush trail;
        private coordinate[] map;
        private double ax, ay;
        private double px, py;
        private double timer;


        //cpu related
        UInt32 cpu_counter;
        int setTime;
        int cpu_pt = 0;
        INSTRUCTION [] instruction;
        int clockspeed = 100; // 100 ms per tick = 10Hz 

        public Form1()
        {
            InitializeComponent();

            //initialize graphical system
            g = PictureBox1.CreateGraphics();
            plane = Brushes.Red;
            trail = Brushes.Yellow;
            plane_led = Brushes.Green;


            //initialize physical system
            map = new coordinate[20];
            start = false;
            timer = 0.0;
            timer1.Enabled = false;
            timer2.Enabled = false;
        
            //initialize simulated cpu
            cpu_pt = -1;
            cpu_counter = 0;
            cpu_timer.Enabled = false;
            instruction = new INSTRUCTION[1000];
            clear_instructions();


            //enable environment noise
            environment_random = true;
            r = new Random();

            //load settings, SOFTWARE TEAM PLEASE DO NOT TOUCH THE CONFIG FILE
            //Result can be far off if settings are changed.
            read_config();
            cpu_timer.Interval = clockspeed;
            position_reset();

            //debug flag
            if (debug)
                lblDEBUG.Visible = true;



            tbLog.Text += "Initalized" + Environment.NewLine;
        }

        void read_config()
        {
            string fileContent = "";
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader("config.inf");
                fileContent = sr.ReadToEnd();

                string [] lines = fileContent.Split('\n');
                string [] temp;

                for (i = 0; i < lines.Length; i++)
                {
                    temp = lines[i].Split('=');
                    switch (temp[0])
                    {
                        case "Starting_Position_X": 
                            {
                                center_x = Convert.ToInt32(temp[1]);
                                break;
                            }
                        case "Starting_Position_Y":
                            {
                                center_y = Convert.ToInt32(temp[1]);
                                break;
                            }
                        case "Random_Environment":
                            {
                                if (temp[1].ToUpper() == "FALSE")
                                    environment_random = false;
                                else
                                    environment_random = true;
                                break;
                            }
                        case "Random_Rate":
                            {
                                random_rate = Convert.ToDouble(temp[1]);
                                break;
                            }
                        case "X_Reactive_Rate":
                            {
                                r_x = Convert.ToDouble(temp[1]);
                                break;
                            }
                        case "Y_Reactive_Rate":
                            {
                                r_y = Convert.ToDouble(temp[1]);
                                break;
                            }
                        case "CPU_Clock_Speed":
                            {
                                clockspeed = Convert.ToInt32(temp[1]);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                }
                sr.Close();
            }
            catch
            {
                System.IO.StreamWriter wr = new System.IO.StreamWriter("config.inf");
                fileContent = "Starting_Position_X" + "=" + center_x.ToString() + Environment.NewLine;
                fileContent += "Starting_Position_Y" + "=" + center_y.ToString() + Environment.NewLine;
                fileContent += "Random_Environment" + "=" + environment_random.ToString() + Environment.NewLine;
                fileContent += "Random_Rate" + "=" + random_rate.ToString() + Environment.NewLine;
                fileContent += "X_Reactive_Rate" + "=" + r_x.ToString() + Environment.NewLine;
                fileContent += "Y_Reactive_Rate" + "=" + r_y.ToString() + Environment.NewLine;
                fileContent += "CPU_Clock_Speed" + "=" + clockspeed.ToString();
                wr.Write(fileContent);
                wr.Close();
            }
        }


        //---------------------------------------------------------------------------------
        //UI Buttons
        //---------------------------------------------------------------------------------

        //load cpu instructions
        private void btnLoadWayPoints_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            string fileContent = "";

           

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    fileContent = sr.ReadToEnd();
                   

                    sr.Close();
                }
                catch 
                {
                    MessageBox.Show("Failed to read the file!");
                    return;
                }




                try
                {

                    string[] temp = fileContent.Split('\n');
                    for (i = 0; i < temp.Length; i++)
                    {
                        INSTRUCTION t = command_set(temp[i]);
                        if (t.timer >= 0)
                        {
                            instruction[i] = t;
                            instruction[i].x /= 10;
                            instruction[i].y /= 10;
                        }
                        else
                        {
                            MessageBox.Show("Failed to compile the file!");
                            return;
                        }
                    }


                }
                catch
                {
                    MessageBox.Show("Failed to compile the file!");
                    return;
                }
            }

        }

        //Flow control button
        private void btnStart_Click(object sender, EventArgs e)
        {

            start = true;
            timer1.Enabled = true;
            timer2.Enabled = true;
            initalize_cpu();
            tbLog.Text += "Start" + Environment.NewLine;

        }

        //Reset button
        private void btnReset_Click(object sender, EventArgs e)
        {
            ax = 0;
            px = 0;
            ay = 0;
            py = 0;
            position_reset();
            timer2.Enabled = true;
            cpu_counter = 0;
            led = false;
            cpu_timer.Enabled = false;
            start = false;
            timer = 0.0;

            tbLog.Text += "Reset" + Environment.NewLine;
        }
        
        //Save log file
        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamWriter wr = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    wr.Write(tbLog.Text);
                    wr.Close();
                }
                catch
                {
                    MessageBox.Show("Failed to write the file!");
                    return;
                }
            }
        }


        //----------------------------------------------------
        //CPU
        //----------------------------------------------------

        //CPU instruction reading
        private void cpu_instruction()
        {
            cpu_counter++;
            if (cpu_counter >= setTime)
            {
                cpu_counter = 0;
                next_instruction();
            }
        }

        private void next_instruction()
        {
            //critical section, disable interrupt
            cpu_timer.Enabled = false;
            cpu_pt++;

            tbLog.Text += lblTime.Text + " Fetch next instruction, pointer at " + cpu_pt.ToString() + Environment.NewLine; 
            if (instruction[cpu_pt].x == 0 && instruction[cpu_pt].y == 0 && instruction[cpu_pt].timer == 0)
            {
                tbLog.Text += lblTime.Text + " Finish executing all the commands, now stop" + Environment.NewLine;

               
                led = false;
             

                cpu_pt = 0;
                setTime = 0;

                ax = 0;
                ay = 0;


                return; 
             }

            ax = instruction[cpu_pt].x;
            ay = instruction[cpu_pt].y;
            setTime = instruction[cpu_pt].timer;
            if (instruction[cpu_pt].led == 0)
                led = false;
            else
                led = true;

            cpu_timer.Enabled = true;
            
        }

        //clear drone's memory
        private void clear_instructions()
        {
            for (i = 0; i < 1000; i++)
            {
                instruction[0].x = 0;
                instruction[0].y = 0;
                instruction[0].timer = 0;
                instruction[0].led = 0;
            }
        }

        //----------------------------------------------------
        //Graphical 
        //----------------------------------------------------

        private void graphical_update()
        {
            g.Clear(Color.Black);
            for (i = 19; i > 0; i-- )
                g.FillEllipse(trail, (int)map[i].x, (int)map[i].y, 10, 10);
            
            if (led)
                g.FillEllipse(plane_led, (int)map[0].x, (int)map[0].y, 10, 10);  
            else
                 g.FillEllipse(plane, (int)map[0].x, (int)map[0].y, 10, 10);
        }

        //Physical system position update
        private void position_update()
        {
            for (i = 19; i > 0; i--)
            {
                map[i] = map[i - 1];
            }

            px = r_x * ax + (1- r_x) * px;
            map[0].x = map[0].x + px + random_x;

            if (map[0].x < ulx)
            {
                map[0].x = ulx;
                plane_crash();
                return;
            }
            else if (map[0].x > brx )
            {
                map[0].x = brx;
                plane_crash();
                return;
            }


            py = r_y * ay + (1 - r_y) * py;
            map[0].y = map[0].y + py + random_y;

            if (map[0].y < uly)
            {
                map[0].y = uly;
                plane_crash();
                return;
            }
            else if (map[0].y > bry)
            {
                map[0].y = bry;
                plane_crash();
                return;
            }
         }

        //reset the physical position
        private void position_reset()
        {
            for (i = 0; i < 20; i++)
            {
                map[i].x = (double)center_x;
                map[i].y = (double)center_y;
            }
        }

        //drone crashed!
        private void plane_crash()
        {
            timer2.Enabled = false;
            cpu_timer.Enabled = false;
            tbLog.Text += lblTime.Text + " Drone has crashed!" + Environment.NewLine;
        }


        //----------------------------------------------------------------------------------
        //Timers
        //----------------------------------------------------------------------------------

        //timer to update the graphcial system
        private void timer1_Tick(object sender, EventArgs e)
        {
            graphical_update();
            if(start)
            timer += 0.05;
            lblTime.Text = timer.ToString("f1")+"s";
        }


        //timer to update the physical system
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (environment_random)
            {
                random_x = (r.NextDouble() - 0.5) * random_rate;
                random_y = (r.NextDouble() - 0.5) * random_rate;
            }

            position_update();
            
        }

        //drone internal cpu clock 
        private void cpu_timer_Tick(object sender, EventArgs e)
        {
            cpu_instruction();
        }

        //initialize drone internal cpu
        private void initalize_cpu()
        {
            cpu_counter = 0;
            cpu_pt = -1;
            setTime = 0;
            cpu_timer.Enabled = true;
        }


        //---------------------------------------------------------------------------------
        //driver layer masking
        //---------------------------------------------------------------------------------
        INSTRUCTION command_set(string input)
        {
            //instructions:
            //MOVEX +/- 100
            //MOVEY +/- 100
            //WAIT

            INSTRUCTION ins;
            ins.x = 0;
            ins.y = 0;
            ins.timer = 0;
            ins.led = 0;
            try
            {
                string[] line = input.Split(' ', '\n', '\r');
                if(debug)
                {
                  //  lblDEBUG.Text = line[0]+" "+line[1]+" "+line[2]+ " " +(Convert.ToInt32(line[1])+Convert.ToInt32(line[2]));
                }
                switch (line[0].ToUpper())
                {
                    case "MOVE": 
                        {
                             ins.x = Convert.ToInt32(line[1]);
                             ins.y = Convert.ToInt32(line[2]);
                            ins.timer = Convert.ToInt32(line[3]);
                            ins.led = Convert.ToInt32(line[4]); 
                            break;
                        }
                    case "WAIT": 
                        { ins.x =  0; 
                            ins.y = 0;
                            ins.timer = Convert.ToInt32(line[1]);
                            ins.led = Convert.ToInt32(line[2]); 
                            break;
                        }
                         default: { ins.timer = -1; break; }
                }
                if (ins.x > 100)
                    ins.x = 100;
                else if (ins.x < -100)
                    ins.x = -100;
                
                if (ins.y > 100)
                    ins.y = 100;
                else if (ins.y < -100) 
                    ins.y = -100;

                if (ins.timer > 30000)
                    ins.timer = 30000;

                if (ins.led !=0)
                    ins.led = 1;

            }
            catch
            {
                ins.timer = -1;
            }


            if (debug)
            {
                lblDEBUG.Text = ins.x + " " + ins.y + " " + ins.timer;
            }

            return ins;
        }



        //---------------------------------------------------------------------------------
    }
}
