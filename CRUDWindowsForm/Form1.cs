﻿using System.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUDWindowsForm 
{
    public partial class Form1 : Form {

        string connectionString = "Data Source=KAMILIA\\KAMILIANURMALA;;Initial Catalog=DbProgramSiswa;Integrated Security=True;";

        public Form1() 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            LoadData();
        }

        private void ClearForm() 
        {
            txtNIM.Clear();
            txtNama.Clear();
            textBox1.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();

            txtNIM.Focus();
        }

        private void LoadData() 
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                try 
                {
                    conn.Open();
                    string query = "SELECT NIM AS [NIM], Nama, Email, Telepon, Alamat FROM Mahasiswa";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvMahasiswa.AutoGenerateColumns = true;
                    dgvMahasiswa.DataSource = dt;
                    ClearForm();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error: " + ex.Message,
                    "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTambah(object sender, EventArgs e) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                try 
                {
                    if (txtNIM.Text == "" || txtNama.Text == "" || textBox1.Text == "" || txtTelepon.Text == "" || txtAlamat.Text == "") 
                    {
                        MessageBox.Show("Harap isi semua data!",
                        "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO Mahasiswa (NIM, Nama, Email, Telepon, Alamat) " +
                       "VALUES (@NIM, @Nama, @Email, @Telepon, @Alamat)";

                    using (SqlCommand cmd = new SqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);

                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0) {
                            MessageBox.Show("Data berhasil ditambahkan!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                            LoadData();
                            ClearForm();
                        }
                        else 
                        {
                            MessageBox.Show("Data gagal ditambahkan!",
                            "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error: " + ex.Message,
                    "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnHapus(object sender, EventArgs e) 
        {
            if (dgvMahasiswa.SelectedRows.Count > 0) 
            {
                DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                                       "Konfirmasi",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes) 
                {
                    using (SqlConnection conn = new SqlConnection(connectionString)) 
                    { 
                        try 
                        {
                            string nim = dgvMahasiswa.SelectedRows[0].Cells["NIM"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM Mahasiswa WHERE NIM = @NIM";

                            using (SqlCommand cmd = new SqlCommand(query, conn)) 
                            {
                                cmd.Parameters.AddWithValue("@NIM", nim);
                                int rowAffected = cmd.ExecuteNonQuery();
                                if (rowAffected > 0) 
                                {
                                    MessageBox.Show("Data berhasil dihapus!",
                                    "Sukses",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                    LoadData();
                                    ClearForm();
                                }
                                else 
                                {
                                    MessageBox.Show("Data tidak ditemukan atau gagal dihapus!",
                                      "Kesalahan",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex) 
                        {
                            MessageBox.Show("Error: " + ex.Message,
                            "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else 
            {
                MessageBox.Show("Pilih data yang akan dihapus!",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void BtnUbah(object sender, EventArgs e) 
        {
            if (txtNIM.Text == "") 
            {
                MessageBox.Show("Pilih data yang akan diubah!",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try 
                {
                    conn.Open();
                    string query = "UPDATE Mahasiswa SET Nama = @Nama, Email = @Email, " +
                       "Telepon = @Telepon, Alamat = @Alamat WHERE NIM = @NIM";
                    using (SqlCommand cmd = new SqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);

                        int rowAffected = cmd.ExecuteNonQuery();

                        if (rowAffected > 0) 
                        {
                            MessageBox.Show("Data berhasil diubah!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal diubah!",
                            "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) { }
                MessageBox.Show("Error: " + ex.Message,
                    "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void BtnRefresh(object sender, EventArgs e) 
        {
            LoadData();
            MessageBox.Show($"Jumlah Kolom: {dgvMahasiswa.ColumnCount}\n Jumlah Baris: {dgvMahasiswa.RowCount}",
            "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMahasiswa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMahasiswa.Rows[e.RowIndex];
                txtNIM.Text = row.Cells[0].Value.ToString();
                txtNama.Text = row.Cells[1].Value?.ToString();
                textBox1.Text = row.Cells[2].Value?.ToString();
                txtTelepon.Text = row.Cells[3].Value?.ToString();
                txtAlamat.Text = row.Cells[4].Value?.ToString();
            }

        }

        private void InitializeComponent()
        {
            this.NIM = new System.Windows.Forms.Label();
            this.Nama = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.Label();
            this.Telepon = new System.Windows.Forms.Label();
            this.Alamat = new System.Windows.Forms.Label();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NIM
            // 
            this.NIM.AutoSize = true;
            this.NIM.Location = new System.Drawing.Point(75, 45);
            this.NIM.Name = "NIM";
            this.NIM.Size = new System.Drawing.Size(38, 20);
            this.NIM.TabIndex = 0;
            this.NIM.Text = "NIM";
            // 
            // Nama
            // 
            this.Nama.AutoSize = true;
            this.Nama.Location = new System.Drawing.Point(75, 90);
            this.Nama.Name = "Nama";
            this.Nama.Size = new System.Drawing.Size(51, 20);
            this.Nama.TabIndex = 1;
            this.Nama.Text = "Nama";
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(75, 135);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(72, 30);
            this.Email.TabIndex = 2;
            this.Email.Text = "Email";
            // 
            // Telepon
            // 
            this.Telepon.AutoSize = true;
            this.Telepon.Location = new System.Drawing.Point(75, 180);
            this.Telepon.Name = "Telepon";
            this.Telepon.Size = new System.Drawing.Size(99, 30);
            this.Telepon.TabIndex = 3;
            this.Telepon.Text = "Telepon";
            // 
            // Alamat
            // 
            this.Alamat.AutoSize = true;
            this.Alamat.Location = new System.Drawing.Point(75, 225);
            this.Alamat.Name = "Alamat";
            this.Alamat.Size = new System.Drawing.Size(89, 30);
            this.Alamat.TabIndex = 4;
            this.Alamat.Text = "Alamat";
            // 
            // txtNIM
            // 
            this.txtNIM.Location = new System.Drawing.Point(225, 45);
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.Size = new System.Drawing.Size(300, 39);
            this.txtNIM.TabIndex = 5;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(225, 90);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(300, 39);
            this.txtNama.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(225, 135);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 39);
            this.textBox1.TabIndex = 7;
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(225, 180);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(300, 39);
            this.txtTelepon.TabIndex = 8;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(225, 225);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(300, 39);
            this.txtAlamat.TabIndex = 9;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(908, 694);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.txtTelepon);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNIM);
            this.Controls.Add(this.Alamat);
            this.Controls.Add(this.Telepon);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.Nama);
            this.Controls.Add(this.NIM);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label NIM;
        private Label Nama;
        private Label Email;
        private Label Telepon;
        private Label Alamat;
        private TextBox txtNIM;
        private TextBox txtNama;
        private TextBox textBox1;
        private TextBox txtTelepon;
        private TextBox txtAlamat;
    }

}