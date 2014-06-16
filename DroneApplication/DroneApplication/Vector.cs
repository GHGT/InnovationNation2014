using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneApplication
{
   public class Vector
    {
       public int x { get; set; }
       public int y { get; set; }
      
       public Vector(int x, int y)
       {
           this.x = x;
           this.y = y;
       }

       public static Vector makePosVector(Vector vector1, Vector vector2)
       {
           Vector vector3 = new Vector(0, 0);
           vector3.x = vector1.x - vector2.x;
           vector3.y = vector1.y - vector2.y;
           return vector3;
       }

       public static string[] makeInstructionSet(Vector posVector)
       {
           string[] instruc = new string[2];
           string left = "left";
           string right = "right";
           string up = "up";
           string down = "down";
           string noCommand = string.Empty;
           switch(posVector.x)
           {
               case 0:
                   instruc[0] = noCommand;
                   break;

               case 1:
                   instruc[0] = right;
                   break;
               case -1:
                   instruc[0] = left;
                   break;
               default:
                   instruc[0] = right;
                   break;
           }

           switch (posVector.y)
           {
               case 0:
                   instruc[1] = noCommand;
                   break;

               case 1:
                   instruc[1] = up;
                   break;
               case -1:
                   instruc[1] = down;
                   break;
               default:
                   instruc[1] = up;
                   break;
           }
           return instruc;
       }
    }
}
