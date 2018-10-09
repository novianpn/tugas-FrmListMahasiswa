using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmListMahasiswa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.Columns.("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.("Npm", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.("Nama", 180, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.("Jenis Kelamin", 800, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.("Tempat Lahir", 75, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.("Tgl. Lahir", 75, HorizontalAlignment.Center);
        }





    }
}
