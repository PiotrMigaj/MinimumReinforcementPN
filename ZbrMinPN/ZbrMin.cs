using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbrMinPN
{
    class ZbrMin
    {
        public double fctm;

        public double Ecm;

        public double ky;

        public double Es;

        public double h;

        public double cnom;

        public double fi;

        public double wlim;

        public double kt;

        public static double a;

        public static double ka;

        public static double hceff;

        public static double Aceff;

        public static double ae;

        public ZbrMin(double fctm, double Ecm, double ky, double Es, double h, double cnom, double fi, double wlim, double kt)
        {
            this.fctm = fctm;
            this.Ecm = Ecm;
            this.ky = ky;
            this.Es = Es;
            this.h = h;
            this.cnom = cnom;
            this.fi = fi;
            this.wlim = wlim;
            this.kt = kt;
        }

        public double zbrMinValueHelper(double As)
        {
            double fcteff;
            fcteff = this.ky * this.fctm;

            //double a;
            a = this.cnom + 0.5 * this.fi;

            double m1;
            m1 = this.h / a;

            //double ka;
            if (m1 <= 5)
            {
                ka = 2.5 * m1 / 5;
            }
            else if (m1 > 5 & m1 <= 30)
            {
                ka = 0.5 / 5 * m1 + 2.0;
            }
            else
            {
                ka = 5;
            }

            //double hceff;
            hceff = Math.Min(ka * a, 0.5 * this.h);

            //double Aceff;
            Aceff = 2 * hceff;

            //double ae;
            ae = this.Es / this.Ecm;



            double f;
            f = (srmax(fi, As, Aceff) * eps(kt, fcteff, As, Aceff, ae, Es) - wlim)*1000;

            return f;



        }

        public double zbrMinValue(double x0,double x1, double Eps, int N)
        {
            double Asreq;
            Asreq =  MigiMath.MetodaSiecznych(zbrMinValueHelper, x0, x1, Eps, N);
            return Asreq;
        }

        public static double srmax(double fi, double As, double Aceff)
        {
            return fi / (3.6 * (As / Aceff));
        }

        public static double eps(double kt, double fcteff, double As, double Aceff, double ae, double Es)
        {
            double Ncr;
            Ncr = fcteff * Aceff;

            double ropeff;
            ropeff = As / Aceff;

            double ss;
            ss = Ncr / As;

            double eps;
            eps = Math.Max(((ss - kt * fcteff / ropeff * (1 + ae * ropeff)) / Es), (0.6 * ss / Es));
            return eps;
        }


        //internal static class HelperClass
        //{
        //    internal static double srmax(double fi, double As, double Aceff)
        //    {
        //        return fi / (3.6 * (As / Aceff));
        //    }

        //    internal static double eps(double kt, double fcteff, double As, double Aceff, double ae, double Es)
        //    {
        //        double Ncr;
        //        Ncr = fcteff * Aceff;

        //        double ropeff;
        //        ropeff = As / Aceff;

        //        double ss;
        //        ss = Ncr / As;

        //        double eps;
        //        eps = Math.Max(((ss - kt * fcteff / ropeff * (1 + ae * ropeff)) / Es), (0.6 * ss / Es));
        //        return eps;
        //    }


        //}


    }

}

