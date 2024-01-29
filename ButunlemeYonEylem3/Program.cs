using System;
using Google.OrTools.LinearSolver;

namespace ProductDistribution
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] talep = { 700, 600, 500, 800, 900, 800 };
            int personelSayisi = 40;
            int ekCalismaSuresiSiniri = 6;
            int personelDegisimiSiniri = 5;

           // Değişkenleri tanımlayın.
            IntVar[] uretimler = new IntVar[6];
            IntVar[] depolamalar = new IntVar[6];
            IntVar[] iseAlinanPersoneller = new IntVar[6];
            IntVar[] iseCikartilanPersoneller = new IntVar[6];

            // Değişkenleri başlatın.
            for (int i = 0; i < uretimler.Length; i++)
            {
                uretimler[i] = solver.IntVar(0, personelSayisi * 20);
                depolamalar[i] = solver.IntVar(0, int.MaxValue);
                iseAlinanPersoneller[i] = solver.IntVar(-personelDegisimiSiniri, personelDegisimiSiniri);
                iseCikartilanPersoneller[i] = solver.IntVar(-personelDegisimiSiniri, personelDegisimiSiniri);
            }

            solver.SetObjective(solver.Sum(talep[i] * uretimler[i] for i in 0..talep.Length), Solver.ObjectiveSense.Maximize);

            for (int i = 0; i < talep.Length; i++)
            {

                solver.AddConstraint(uretimler[i] + depolamalar[i] == talep[i]);
                solver.AddConstraint(depolaramalar[i] <= uretimler[i] - ekCalismaSuresiSiniri * personelSayisi);

                if (i == 0)
                {
                    solver.AddConstraint(depolaramalar[i] == 0);
                }
                else if (i == talep.Length - 1)
                {
                    solver.AddConstraint(depolaramalar[i] == 0);
                }

                solver.AddConstraint(iseAlinanPersoneller[i] - iseCikartilanPersoneller[i] <= personelDegisimiSiniri);
                solver.AddConstraint(iseAlinanPersoneller[i] - iseCikartilanPersoneller[i] >= -personelDegisimiSiniri);
            }

            solver.Solve();

            Console.WriteLine("Toplam gelir: " + solver.Objective().Value());
            for (int i = 0; i < uretimler.Length; i++)
            {
                Console.WriteLine("Ay {0}: Üretim {1}, Depolama {2}, İşe alınan personel {3}, İşten çıkarılan personel {4}", i + 1, uretimler[i].Value(), depolamalar[i].Value(), iseAlinanPersoneller[i].Value(), iseCikartilanPersoneller[i].Value());
            }
        }
    }
}