using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Работа_CS_104
{
    internal class Program
    {

        class carIsDeadException : Exception
        {
            private string carName;

            public carIsDeadException() { }
            public carIsDeadException(string carName)
            {
                this.carName = carName;
            }

            public override string Message
            {
                get
                {
                    // Выдано исключение типа "Работа_CS_104.Program+carIsDeadException".
                    string msg = base.Message;

                    if (carName != null)
                    {
                        msg += "\n" + carName + " has been destroyed";
                    }
                    return msg;
                }
            }
        }

        class carIsDeadException2 : Exception
        {
            public carIsDeadException2() { }

            public carIsDeadException2(string message) : base(message) { } 
        }

        class carInvalidSpeedUp : Exception
        {
            public carInvalidSpeedUp() { }
            
            public carInvalidSpeedUp(string message) : base (message) { }
        }


        class car
        {
            private Radio theMusicBox = new Radio();

            private int speed;
            private int maxSpeed;
            private string petName;
            bool dead;

            public car()
            {
                maxSpeed = 100;
                dead = false;
            }

            public car(string name, int max, int curr)
            {
                maxSpeed = max;
                speed = curr;
                petName = name;
                dead = false;
            }

            public void speedUp (int delta) 
            {
                if (dead)
                {
                    //Console.WriteLine(petName + " is dead....");
                    //throw new Exception("Exception: This car is already dead");
                    //throw new carIsDeadException(this.petName);
                    throw new carIsDeadException2(this.petName + " is dead now");
                }
                else
                {
                    if (delta < 0)
                    {
                        throw new ArgumentOutOfRangeException("Must be greater then zero");
                    }

                    else if (delta > 50) 
                    {
                        throw new carInvalidSpeedUp("Invalid acceleration for " + petName);
                    }

                    speed += delta;
                    if (speed < maxSpeed)
                    {
                        Console.WriteLine("\tCurrent Speed = " + speed);
                    }
                    else
                    {
                        Console.WriteLine(petName + " has been overheated....");
                        dead = true;
                    }

                }
            }

            public void Tune(bool state)
            {
                theMusicBox.TurnOn(state);
            }
        }

        class Radio
        {
            public Radio() { }

            public void TurnOn(bool on)
            {
                if (on) 
                {
                    Console.WriteLine("Radio is now on");
                }
                else { Console.WriteLine("Radio is now quiet");}
            }

            ~Radio()
            {
                Console.WriteLine("Radio is now Destroyed");
            }
        }





        static void Main(string[] args)
        {
            car c1 = new car("bobi", 100, 0);

            //try
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        c1.speedUp(20);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    //Console.WriteLine(e.StackTrace);
            //}

            c1.Tune(true);

            try
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    c1.speedUp(-20);
                //}

                //for (int i = 0; i < 10; i++)
                //{
                //    c1.speedUp(60);
                //}

                for (int i = 0; i < 10; i++)
                {
                    c1.speedUp(30);
                }
            }

            catch (ArgumentOutOfRangeException e) { Console.WriteLine("ArgumentOutOfRangeException" + e.Message); }
            catch (carInvalidSpeedUp e) { Console.WriteLine("carInvalidSpeedUp" + e.Message); }
            catch (carIsDeadException2 e) { Console.WriteLine("carIsDeadException2" + e.Message); }

            finally
            {
                Console.WriteLine("Finally in");
                c1.Tune(false);
                Console.WriteLine("Finally out");
            }
        }
    }
}
