using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft.Common
{
    public class GaussianRandom
    {
        public static double[] RandomArray(int amount,int len){
            int supplement = 5; //高斯分布偏移量
            double[] seq = new double[len];
            GaussianRng gr = new GaussianRng();
            int limit = 4; //值范围
            for (int i = 0; i < len; i++)
            {
                double result = 0;
                double num = gr.Next() + supplement;
                if (num < (supplement - limit))
                {
                    result = supplement - limit;
                }
                else if (num > (supplement + limit))
                {
                    result = supplement + limit;
                }
                else
                {
                    result = num;
                }
                seq[i] = result;
            }
            seq = Normalization(seq);
            double rs = 0;
            double[] final = new double[seq.Length];
            for (int k = 0; k < seq.Length; k++)
            {
                double sm = Math.Round(seq[k] * amount, 2);
                final[k] = sm;
                if (k == (seq.Length - 1))
                {
                    double dvalue = amount - (rs + sm);
                    final[k] = Math.Round(final[k] + dvalue, 2);
                    rs += final[k];
                }
                else
                {
                    rs += sm;
                }
            }
            return final;
        }

        /// <summary>
        /// 归一化函数
        /// </summary>
        protected static double[] Normalization(double[] ip)
        {
            double sum = 0;
            double[] op = new double[ip.Length];
            foreach (double d in ip)
            {
                sum += d;
            }

            for (int i = 0; i < ip.Length; i++)
            {
                op[i] = ip[i] / sum;
            }
            return op;
        }
    }

    /// <summary>
    /// 高斯分布类
    /// </summary>
    public class GaussianRng
    {
        int iset;
        double gset;
        Random r1, r2;

        public GaussianRng()
        {
            r1 = new Random(unchecked((int)DateTime.Now.Ticks));
            r2 = new Random(~unchecked((int)DateTime.Now.Ticks));
            iset = 0;
        }

        public double Next()
        {
            double fac, rsq, v1, v2;
            if (iset == 0)
            {
                do
                {
                    v1 = 2.0 * r1.NextDouble() - 1.0;
                    v2 = 2.0 * r2.NextDouble() - 1.0;
                    rsq = v1 * v1 + v2 * v2;
                } while (rsq >= 1.0 || rsq == 0.0);

                fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq);
                gset = v1 * fac;
                iset = 1;
                return v2 * fac;
            }
            else
            {
                iset = 0;
                return gset;
            }
        }
    }

}
