using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroneApplication
{
    public partial class Form1 : Form
    {
        List<string> listString  = new List<string>();
        List<Vector> posVectorList = new List<Vector>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != string.Empty)
            {
                parseText textToParse = new parseText(txtInput.Text);
               List<string> listofStuff = textToParse.removeUselessCrap();
               List<Vector> vectorList = new List<Vector>();
                foreach(string item in listofStuff)
                {
                    string[] thisString = item.Split(',');
                    vectorList.Add(new Vector(Int32.Parse(thisString.GetValue(0).ToString()), Int32.Parse(thisString.GetValue(1).ToString())));
                }
                for (int i = 0; i < vectorList.Count - 1 ; i++)
                {
                    posVectorList.Add(Vector.makePosVector(vectorList[i], vectorList[i + 1]));
                }
                foreach(Vector pos in posVectorList)
                {
                    listString.Add(Vector.makeInstructionSet(pos).GetValue(0).ToString());
                    listString.Add(Vector.makeInstructionSet(pos).GetValue(1).ToString());
                }
             foreach(string x in listString)
             {
                 txtOutput.AppendText(x + ", ");
             }
                //removes last comma and space
             txtOutput.Text.Substring(0, txtOutput.Text.Length - 3);
            }
        }
    }
}
