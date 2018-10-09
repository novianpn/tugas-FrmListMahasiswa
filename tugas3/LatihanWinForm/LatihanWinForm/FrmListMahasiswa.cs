using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LatihanWinForm
{
    public partial class FrmListMahasiswa : Form
    {

        public delegate void SaveUpdateEventHandler(Mahasiswa obj);
        private bool isNewData = true;
        private Mahasiswa mhs = null;
        private IList<Mahasiswa> listOfMahasiswa = new List<Mahasiswa>();

        public FrmListMahasiswa()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        public void Form12(Mahasiswa obj)
        {

            this.isNewData = false;
            this.mhs = obj;

            mskNpm.Text = this.mhs.Nim;
            txtNama.Text = this.mhs.Name;

            if (this.mhs.Gender == "Laki-laki")
                rdoLakilaki.Checked = true;
            else
                rdoPerempuan.Checked = true;

            
            txtTempatLahir.Text = this.mhs.Born;
            dtpTanggalLahir.Value = DateTime.Parse(this.mhs.Date);
        }


        private void InisialisasiListView()
        {
            lvwMahasiswa.View = System.Windows.Forms.View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Npm", 70, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Nama", 180, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Jenis Kelamin", 80, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Tempat Lahir", 75, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Tgl. Lahir", 75, HorizontalAlignment.Left);
        }


        private void FillToListView(bool isNewData, Mahasiswa mhs)
        {
            if (isNewData)
            {
                int noUrut = lvwMahasiswa.Items.Count + 1;

                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Nim);
                item.SubItems.Add(mhs.Name);
                item.SubItems.Add(mhs.Gender);
                item.SubItems.Add(mhs.Born);
                item.SubItems.Add(mhs.Date);
                

                lvwMahasiswa.Items.Add(item);
            }
            else
            {
                int row = lvwMahasiswa.SelectedIndices[0];

                ListViewItem itemRow = lvwMahasiswa.Items[row];
                itemRow.SubItems[1].Text = mhs.Nim;
                itemRow.SubItems[2].Text = mhs.Name;
                itemRow.SubItems[3].Text = mhs.Gender;
                itemRow.SubItems[4].Text = mhs.Born;
                itemRow.SubItems[5].Text = mhs.Date;
               
            }
        }


        private void Form1_OnSave(Mahasiswa obj)
        {
            listOfMahasiswa.Add(obj);
            FillToListView(true, obj);
        }

        private void LvwMahasiswa_DoubleClick(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var mhs = listOfMahasiswa[lvwMahasiswa.SelectedIndices[0]];
                Form12(mhs);

            }
              else
            {
                MessageBox.Show("Silahkan Pilih Data Dahulu", "Peringatan!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (!mskNpm.MaskFull)
            {
                MessageBox.Show("Nim Harus diisi", "Konfirmasi", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                return;
            }
            if (!(txtNama.Text.Length > 0))
            {
                MessageBox.Show("Nama Harus diisi", "Konfirmasi", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                return;
            }
            var msg = string.Format("Apakah data mahasiswa '{0}' ingin disimpan ?", txtNama.Text);

            if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if (isNewData)
                    mhs = new Mahasiswa();

                mhs.Nim = mskNpm.Text;
                mhs.Name = txtNama.Text;
                mhs.Gender = rdoLakilaki.Checked ? "Laki-laki" : "Perempuan";
                mhs.Born = txtTempatLahir.Text;
                mhs.Date = dtpTanggalLahir.Value.ToString("dd/MM/yyyy");
                if (isNewData) // data baru
                {
                    Form1_OnSave(mhs);
                    ResetForm();
                }
                else
                {
                    Form1_OnUpdate(mhs);// panggil event OnUpdate
                    ResetForm();
                    isNewData = true;
                }
            }

        }

        private void ResetForm()
        {

            mskNpm.Clear();
            txtNama.Clear();
            rdoLakilaki.Checked = true;
            txtTempatLahir.Clear();
            dtpTanggalLahir.Value = DateTime.Today;

            mskNpm.Focus();
        }

        private void Form1_OnUpdate(Mahasiswa obj)
        {
            listOfMahasiswa.Add(obj);
            FillToListView(false, obj);
        }
        


        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var mhs = listOfMahasiswa[lvwMahasiswa.SelectedIndices[0]];

                var msg = string.Format("Apakah data mahasiswa '{0}' ingin dihapus ?", mhs.Name);

                if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    listOfMahasiswa.Remove(mhs);

                    lvwMahasiswa.Items.Clear();
                    foreach (var obj in listOfMahasiswa)
                    {
                        FillToListView(true, obj);
                    }
                }
            }
            else
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void BtnTutup_Click(object sender, EventArgs e)
        {
            var msg = "Apakah Anda Yakin ?";

            var result = MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
                this.Close();
        }
    }
}
