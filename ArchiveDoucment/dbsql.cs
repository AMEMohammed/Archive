using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace ArchiveDoucment
{
    class DBSQL
    {


        // private string ConnectionSreing = @"Data Source=.\s2008;Initial Catalog=Archive;UserId=";
         private string ConnectionSreing = @"Data Source=.\s2008;Initial Catalog=Archive; User Id=onlyarchive; password=123456";
        //   private string ConnectionStriingMaster = @"Data Source=" + Properties.Settings.Default.nmserver + ";Initial Catalog=master;Integrated Security=True";
     
        public SqlConnection con;
        public SqlCommand cmd;

        public SqlDataAdapter adapter;
        public DBSQL()
        {
            con = new SqlConnection(ConnectionSreing);
        }
        ///  exuectue  qury ansert  and delete and update
        /// </summary>
        /// <param name="qury"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int ExecuteQury(string qury, int id, string name, int flag)
        {
            int res = 0;
            try
            {

                con.Open();

                cmd = new SqlCommand(qury, con);
                cmd.CommandType = CommandType.Text;

                if (flag == 1) //insert
                {
                    cmd.Parameters.AddWithValue("@name", name);

                }
                if (flag == 2) //update
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);

                }
                else if (flag == 3)//delete
                {
                    cmd.Parameters.AddWithValue("@id", id);

                }
                try
                {
                    res = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return res;
        }

        /////////////////////////
        ///// Add TypeDoucment
        public int AddTypeDoucment(string type)
        {
            string qury = "insert into DoucmentType (NameType) values(@name)";
            return ExecuteQury(qury, 0, type, 1);
        }

        ////////////////////////////
        ///////// update Type Doucment
        public int UpdateTypeDoucment(int id, string newName)
        {
            string qury = "update  DoucmentType set NameType =@name where IdType=@id";
            return ExecuteQury(qury, id, newName, 2);
        }
        ////////////////////
        ///// Delete TypeDouCment
        public int DeleteTypeDoucment(int id)
        {
            string qury = "delete from DoucmentType where IdType=@id";
            return ExecuteQury(qury, id, null, 3);
        }
        //////////////
        //// getAll TypeDoucment
        public DataTable GetAllTypeDoucment()
        {
            DataTable dt = new DataTable();

            adapter = new SqlDataAdapter("select IdType as 'الرقم' ,NameType as 'النوع' from DoucmentType", con);
            adapter.Fill(dt);
            return dt;
        }

        /////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////
        // Organization

        ////// add Organization
        public int AddNewOrganization(string name)
        {
            string qury = "insert into Organization (NameOrga) values(@name)";
            return ExecuteQury(qury, 0, name, 1);
        }
        ////////////////////////////////////////////////////////////////////////////
        ///////// Update Organization
        public int UpdateOrganization(int id, string name)
        {
            string qury = "update Organization set NameOrga=@name where IdOrga=@id";
            return ExecuteQury(qury, id, name, 2);
        }
        /////////////////////////////////////////////////////////////////////////////
        ///////  delete Organization
        public int DeleteOrganization(int id)
        {
            string qury = "delete from Organization where IdOrga=@id";
            return ExecuteQury(qury, id, null, 3);
        }
        ////////////////////////////////////////////////////////////////////////////////
        //// get all Organization
        public DataTable GetAllOrganization()
        {
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("select IdOrga as'الرقم' ,NameOrga as 'الجهة' from Organization", con);
            adapter.Fill(dt);
            return dt;

        }

        ///////////////////////////////////
        /////////////////////////////
        /// doucment
        /// add new doucment
        public int AddNewDoucment(int idtype, int idor, string doucment, DateTime d1, string dsc, byte[] imageDou)
        {
            int res = 0;
            try
            {

                cmd = new SqlCommand("insert into Doucment (IdType,IdOr,Doucment,SaveDate,descr,DoucmentImag) values(@IdType,@IdOr,@Doucment,@SaveDate,@descr,@DoucmentImag)", con);
                cmd.Parameters.AddWithValue("@IdType", idtype);
                cmd.Parameters.AddWithValue("@IdOr", idor);
                cmd.Parameters.AddWithValue("@Doucment", doucment);
                cmd.Parameters.AddWithValue("@SaveDate", d1);
                cmd.Parameters.AddWithValue("@descr", dsc);
                cmd.Parameters.AddWithValue("@DoucmentImag", imageDou);
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            {
                con.Close();
            }
            return res;
        }

        //////////////////
        /////// update doucment
        public int UpdateDoucment(int idtype, int idor, string doucment, string dsc,int idDo)
        {

            int res = 0;
            try
            {
                cmd = new SqlCommand("update Doucment set IdType=@IdType ,IdOr=@IdOr,Doucment=@Doucment,descr=@descr where IdDoucment=@IdDoucment", con);
                cmd.Parameters.AddWithValue("@IdType", idtype);
                cmd.Parameters.AddWithValue("@IdOr", idor);
                cmd.Parameters.AddWithValue("@Doucment", doucment);
             
                cmd.Parameters.AddWithValue("@descr", dsc);
          
                cmd.Parameters.AddWithValue("@IdDoucment", idDo);
                cmd.CommandType = CommandType.Text;
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            {
                con.Close();
            }
            return res;
        }
        ////////////////////////////
        ///////////// delete doucment
   public     int deleteDoucment(int iddou)
        {

            int res = 0;
            try
            {
                cmd = new SqlCommand("delete from Doucment where IdDoucment=@IdDoucment ", con);
                cmd.Parameters.AddWithValue("@IdDoucment", iddou);
                cmd.CommandType = CommandType.Text;

                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return res;

        }
      
        


       
         ///////////
         //////////
         /// get doucment by date
         /// 
         public DataTable SearchDoucmentByDate(string IdTy,string IdOr,string Name,DateTime d1,DateTime d2)
        {
            DataTable dt = new DataTable();
            IdTy = "%" + IdTy;
            IdOr = "%" + IdOr;
            Name = "%" + Name + "%";
            cmd = new SqlCommand("select Doucment.IdDoucment as 'رقم' ,Organization.NameOrga as'الجهة' ,DoucmentType.NameType as 'النوع', Doucment.Doucment as 'اسم المستند' ,Doucment.SaveDate as 'تاريخ الحفظ' ,Doucment.descr as 'ملاحظات'  from Doucment,DoucmentType,Organization where Doucment.IdType=DoucmentType.IdType and Doucment.IdOr=Organization.IdOrga and CONVERT(nvarchar,Doucment.IdType) like @idtype and CONVERT(nvarchar,Doucment.IdOr) like @idor and Doucment.Doucment like @name  and Doucment.SaveDate between @d1 and @d2", con);
            cmd.Parameters.AddWithValue("@idtype",IdTy);
            cmd.Parameters.AddWithValue("@idor",IdOr);
            cmd.Parameters.AddWithValue("@name",Name);
            cmd.Parameters.AddWithValue("@d1",d1);
            cmd.Parameters.AddWithValue("@d2",d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        ///////////
        //////////
        /// get doucment
        /// 
        public DataTable SearchDoucment(string IdTy, string IdOr, string Name)
        {
            DataTable dt = new DataTable();
            IdTy = "%" + IdTy;
            IdOr = "%" + IdOr;
            Name = "%" + Name + "%";
            cmd = new SqlCommand("select Doucment.IdDoucment as 'رقم' ,Organization.NameOrga as'الجهة' ,DoucmentType.NameType as 'النوع', Doucment.Doucment as 'اسم المستند' ,Doucment.SaveDate as 'تاريخ الحفظ' ,Doucment.descr as 'ملاحظات'  from Doucment,DoucmentType,Organization where Doucment.IdType=DoucmentType.IdType and Doucment.IdOr=Organization.IdOrga and CONVERT(nvarchar,Doucment.IdType) like @idtype and CONVERT(nvarchar,Doucment.IdOr) like @idor and Doucment.Doucment like @name ", con);
            cmd.Parameters.AddWithValue("@idtype", IdTy);
            cmd.Parameters.AddWithValue("@idor", IdOr);
            cmd.Parameters.AddWithValue("@name", Name);
            
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ///
        ////
        // getImage
        public Image GetImage(int IdDou)
        {
        
            cmd = new SqlCommand(" select DoucmentImag from Doucment where IdDoucment=@id", con);
           cmd.Parameters.AddWithValue("@id", IdDou);
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            byte[] b = (byte[])(dt.Rows[0][0]);
            MemoryStream ms = new MemoryStream(b);
            return Image.FromStream(ms);
          
        }
        ///////
        // getDoucment
        public DataTable GetDoucmentSingle(int iddoument)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(" select Doucment.IdDoucment ,Doucment.Doucment,Doucment.IdOr,Doucment.IdType,Doucment.descr from Doucment  where Doucment.IdDoucment =@id", con);
            cmd.Parameters.AddWithValue("@id", iddoument);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //// buck up database
        public int BuckUpdatabase(string path)
        {

            int res = 0;

            cmd = new SqlCommand("BACKUP DATABASE Archive  TO DISK=@d", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d", path);
            con.Open();
            res = cmd.ExecuteNonQuery();




            con.Close();


            return res;

        }



    }
}
