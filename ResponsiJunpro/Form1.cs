using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql;

namespace ResponsiJunpro
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection connect;
        public DataTable dt;
        public DataTable dt2;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=responsiDafrom";
        public static NpgsqlCommand cmd;
        public static NpgsqlCommand cmd2;
        private string sqlstr = null;
        private string sqlstr2 = null;

        public DataGridViewRow row;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connect = new NpgsqlConnection(connstring);
            connect.Open();

            sqlstr = "SELECT * FROM public.karyawan";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sqlstr, connect);

            NpgsqlDataReader read = cmd.ExecuteReader();
            dt.Load(read);
            dgvTable.DataSource = dt;
            connect.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            connect.Open();

            sqlstr = "INSERT INTO public.karyawan (id_karyawan, nama, id_dep) VALUES (::character, ::character varying, ::integer);";
            cmd = new NpgsqlCommand(sqlstr, connect);
            cmd.Parameters.AddWithValue("::character" + tbID.Text);
            cmd.Parameters.AddWithValue("::character varying" + tbNama.Text);
            cmd.Parameters.AddWithValue("::integer" + tbDept.Text);

            cmd.ExecuteScalar();
            dgvTable.DataSource = dt;
            connect.Close();
            btnLoad.PerformClick();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connect.Open();
            if (row == null);
            sqlstr = "DELETE FROM public.karyawan WHERE id_karyawan IN ();";
        }

        public void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                row = dgvTable.Rows[e.RowIndex];

                tbID.Text = row.Cells["character"].Value.ToString();
                tbNama.Text = row.Cells["character varying"].Value.ToString();
                tbDept.Text = row.Cells["integer"].Value.ToString();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            connect = new NpgsqlConnection(connstring);
            connect.Open();

            sqlstr = "SELECT * FROM public.karyawan";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sqlstr, connect);

            NpgsqlDataReader read = cmd.ExecuteReader();
            dt.Load(read);
            dgvTable.DataSource = dt;

            sqlstr2 = "SELECT * FROM public.departemen";
            dt2 = new DataTable();
            cmd2 = new NpgsqlCommand(sqlstr2, connect);

            NpgsqlDataReader read2 = cmd2.ExecuteReader();
            dt2.Load(read2);
            dgvDept.DataSource = dt2;
            connect.Close();
        }
    }
}
