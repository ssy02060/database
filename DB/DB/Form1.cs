using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            oracleCommand1.CommandText = "SELECT * FROM EMP";
            oracleCommand2.CommandText = "SELECT ename FROM emp WHERE empno = '1'";
            oracleCommand3.CommandText = "SELECT * FROM EMP WHERE EMPNO = :PARAM";
            
        }
        //ExcuteReader(); select 반환값 여러개 가능
        //ExcuteScalar(); select 반환값 한 개만 가능 (count와 같으 명령어)
        //ExcuteNonQuery(); insert, Delete, update를 사용할때

        //OracleDataReader : Cursor의 개념 첫번째 record 앞을 가리킨다. read 메서드를 호출할때마다 한개씩 읽어온다.
        //데이터를 다읽어오면 False
        //rdr 이라는 커서는 위에서 밑으로
        //column의 위치를 정확하게 안다면 "rdr[숫자]"로 표현 가능

           
        private void button1_Click(object sender, EventArgs e)
        {
            oracleConnection1.Open();
            MessageBox.Show("연결성공");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleDataReader rdr = oracleCommand1.ExecuteReader();

            /*
             * ExcuteScalar 실습
             * int result = Convert.ToInt32(oracleCommand1.ExecuteScalar());
            listBox1.Items.Add("전체 수 : " + result.ToString());
            listBox1.Items.Add("empno = 1의 이름: " + oracleCommand2.ExecuteScalar());
            */
            int no1 = rdr.GetOrdinal("EMPNO");
            int no2 = rdr.GetOrdinal("ENAME");
            int no3 = rdr.GetOrdinal("JOB");

            while (rdr.Read())
            { 
                listBox1.Items.Add(rdr[no1].ToString() + " " + rdr[no2].ToString() + " " + rdr[no3].ToString());
            }
            rdr.Close();
            
        }

        private void serch_Click(object sender, EventArgs e)
        {
            oracleCommand3.Parameters[0].Value = textBox1.Text;
            OracleDataReader rdr = oracleCommand3.ExecuteReader();
            listBox1.Items.Clear();

            while (rdr.Read())
                listBox1.Items.Add(rdr["ENAME"]);
            rdr.Close();
        }
    }
}
