using System;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace TubesKPL
{
    class Program
    {
        //table driven
        enum caraBayar
        {
            COD,
            TFBank,
            qris

        }

        static string getPenjelasan(caraBayar a)
        {
            string[] metode = { "silahkan menunggu barang sampai ke tangan anda lalu bayar", "pilih bank lalu masukkan 9 digit angka dari nomor rekening", "ctrl + klik pada Link untuk mendapatkan kode QR: " + "https://drive.google.com/file/d/1H4Yfvc_75mjGq_-z0Rf4kHVeCTZP4XnB/view" };

            int indeks = (int)a;
            return metode[indeks];
        }

        //eksekusi
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Pilih Cara Pembayaran:");
            Console.WriteLine("1. Cash on delivery");
            Console.WriteLine("2. Transfer Bank");
            Console.WriteLine("3. QRIS");
            Console.WriteLine(" ");
            Console.Write("Masukkan angka menu: ");
            string inputs = Console.ReadLine();
            Console.WriteLine(" ");


            if (inputs == "1")
            {
                string CaraBayar = getPenjelasan(caraBayar.COD);
                Console.WriteLine(CaraBayar);
            }
            else if (inputs == "2")
            {
                string CaraBayar = getPenjelasan(caraBayar.TFBank);
                Console.WriteLine(CaraBayar);
                pilihBank();
                kejelasan();
            }
            else if (inputs == "3")
            {
                string CaraBayar = getPenjelasan(caraBayar.qris);
                Console.WriteLine(CaraBayar);
            }
            else
            {
                Console.WriteLine("Pilih pembayaran yang tersedia di menu");
            }
        }

        static void kejelasan()
        {
            Console.WriteLine("Masukkan nomor rekening");
            string input = Console.ReadLine();

            //desain by contract
            Debug.Assert(input.Length == 9, "Nomor kartu tidak memenuhi standar");

            //error handling
            try
            {
                int inputcek = Convert.ToInt32(input);
                Console.WriteLine("Terimakasih Pembayaran akan segera kami proses dilanjutkan dengan pengiriman barang anda");
            }
            catch (FormatException e)
            {
                Console.WriteLine("input harus berupa angka");
            }
        }

        //---------------------------------------------TEKNIK RUNTIME CONFIGURATION------------------------------------
        static void pilihBank()
        {
            
            List<biayaAdmin> listBiaya = new List<biayaAdmin>();
            listBiaya.Add(new biayaAdmin(" ", 0));
            listBiaya.Add(new biayaAdmin("BCA", 2500));
            listBiaya.Add(new biayaAdmin("BNI", 2000));
            listBiaya.Add(new biayaAdmin("BRI", 1500));
            listBiaya.Add(new biayaAdmin("MANDIRI", 1000));

            mengserialisasi(listBiaya);

            List<biayaAdmin> listbiaya = mengdeserialisasi<List<biayaAdmin>>("bank.json");
            
            Console.WriteLine(" ");
            Console.WriteLine("1. BCA");
            Console.WriteLine("2. BNI");
            Console.WriteLine("3. BRI");
            Console.WriteLine("4. MANDIRI");
            Console.WriteLine(" ");
            Console.Write("Masukkan angka dari list bank: ");

            string i = Console.ReadLine();

            //DESAIN BY CONTRACT
            Debug.Assert(i.Length == 1, "Nomor kartu tidak memenuhi standar");
            
            //ERROR HANDLING
            try
            {
                int inputcek = Convert.ToInt32(i);
                while (inputcek <= 4 )
                {
                    Console.WriteLine("nama bank : " + listbiaya[inputcek].NamaBank + " | " + "biaya admin: " + listbiaya[inputcek].Biaya);
                    Console.WriteLine(" ");
                    break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("input harus berupa angka");
            }
        }

        private static void mengserialisasi(Object obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string hasil = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(@"D:\vscode\C#\TubesKPL\TubesKPL\bank.json", hasil);
        }

        private static Tipe mengdeserialisasi<Tipe>(string input)
        {
            string jsonString = File.ReadAllText(@"D:\vscode\C#\TubesKPL\TubesKPL\bank.json");
            return JsonSerializer.Deserialize<Tipe>(jsonString);
        }
    }

    public class biayaAdmin
    {
        //PARSING
        public string NamaBank { get; set; }
        public int Biaya { get; set; }

        public biayaAdmin() { }

        public biayaAdmin(string nama, int biaya)
        {
            this.NamaBank = nama;
            this.Biaya = biaya;

        }
    }
    //-------------------------------------------------------------------------------------------------------------------
}

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//tester
// 
/*string nomoratm = Console.ReadLine();
                    int a = nomoratm.Length;*/

//precondition
/*                Debug.Assert(a > 16, "Nomor ATM tidak Valid, tolong pastikan kembali nomor ATM yang anda gunakan");
                Debug.Assert(a < 16, "Nomor ATM tidak Valid, tolong pastikan kembali nomor ATM yang anda gunakan");*/
/*                                 
                    "Terimakasih Pembayaran dan Pengiriman akan segera kami proses"
 */
//error handling
//try
//{

/*bool c = checked(a == 16);
Console.WriteLine("Terimakasih Pembayaran dan Pengiriman akan segera kami proses");*/
//}
//catch
//{
//Console.WriteLine("Nomor ATM harus 16 digit");
//}