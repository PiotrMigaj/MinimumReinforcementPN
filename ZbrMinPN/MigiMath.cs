using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbrMinPN
{
    public delegate double MojDelegat(double x);
    class MigiMath
    {

        public static double MetodaSiecznych(MojDelegat f, double x0, double x1, double e, int N)
        {
            double a, b;
            a = x0;
            b = x1;

            double wynik;
            wynik = 0;
            
            for(int i=0;i<N;i++)
            {
                double x2;
                x2 = b - (f(b) * (b - a)) / (f(b) - f(a));
                if(Math.Abs(f(x2))<=e)
                {
                    wynik = x2;
                }
                else
                {
                    b = a;
                    a = x2;
                }
            }

            return wynik;
        }

        
    }
}
