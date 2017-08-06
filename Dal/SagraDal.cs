        using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text;


namespace SagraElCoda.Dal
{
    public class ComandaItem
    {
     public  int IdAlimento {get;set;}
     public  int IdComanda {get;set;}
     public  int Quant {get;set;}
     public double totPrice { get; set; }
     public int quantRimasta { get; set; }
    }

    public class ComandaPrinc
    {
     public  DateTime dataarr {get;set;}
     public  int IdComanda {get;set;}
     public  int ncoperti {get;set;}
     public double totPriceComanda { get; set; }
    }

    public class SagraDal
    {

       MySql.Data.MySqlClient.MySqlConnection _connection;


        public SagraDal()
        {
            string myConn = ConfigurationManager.ConnectionStrings["mysqlVotazioni"].ConnectionString;
            _connection = new MySqlConnection(myConn);
           
            
        }
         public System.Data.DataTable GetAllAlimentistatisticheXMenuXcategoria(int iddemenu, int idcateg)
        {
            string sql = @"   select a.* from alimento a inner join menualimento m on m.idalimento = a.id and 
m.idmenu=" + iddemenu.ToString() + " and  a.id_categoria="
           +idcateg.ToString();

            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

         public System.Data.DataTable GetAllAlimentistatisticheXMenusXcategoria(string iddemenu, int idcateg)
         {
             string sql = @"   select a.* from alimento a inner join menualimento m on m.idalimento = a.id and 
m.idmenu in (" + iddemenu.ToString() + ") and a.id_categoria="
            + idcateg.ToString();

             return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
         }




        
          public System.Data.DataTable GetAllAlimentistatisticheXCategoria()
        {
            string sql = @"     
        select oc.data,c.Descrizione, sum(f.qta) as totale,sum(f.prezzo) as prezzo from alimento a inner join categoria c on c.id = a.id_categoria
inner join comanda_alimento f on a.id = f.id_alimento inner join  comanda oc on oc.id_comanda = f.id_comanda
group by a.id_categoria, oc.`data`
order by oc.data
 
";

            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

          public System.Data.DataTable GetAllMenu()
          {
              string sql = @"SELECT * FROM MENU;";
              return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
          }

         public System.Data.DataTable GetAllAlimentistatistiche()
        {
            string sql = @"     select date(com.`data`) ,cate.Descrizione as categoria, a.descrizione, SUM(c.qta) as totaleVenduti, sum(c.prezzo) as totIncasso from alimento a inner join comanda_alimento c 
                                on c.id_alimento = a.id  inner join comanda com on com.id_comanda = c.id_comanda inner join categoria cate on cate.id = a.id_categoria
                                group by   date(com.`data`),  a.id_categoria , c.id_alimento ;";

            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

          public System.Data.DataTable GetTotaleComandaPerGiorno()
        {
            string sql = @"      select data,sum(totalecomanda) as totincasso from comanda 
                                    group by comanda.data
                                    ";

            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

          public System.Data.DataTable GetTotaleCopertiComandaPerGiorno()
        {
            string sql = @" select data,sum(n_coperti) from comanda group by comanda.data";

            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

          public System.Data.DataTable GetAlimentiAttiviPerGiorno()
          {
              string sql = @" select c.descrizione, a.descrizione, a.prezzo  
from alimento a inner join categoria c on  a.id_categoria = c.id 
where a.attivo=1
order by c.id  ";

              return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
          }
        

         public System.Data.DataTable GetQuantitRimaste(   )
        {
            string sql = @" select descrizione, quantit as PorzioniRimaste from alimento order by quantit asc";
            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

         public System.Data.DataTable GetAllAlimenti()
         {
             string sql = @"SELECT id,descrizione , attivo from alimento ";
             return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
         }


         public void AggiornaAlimentoAttivo(int idal, int attivo)
         {
             try
             {
                 _connection.Open();
                 string sql = @"update alimento set attivo =" + attivo.ToString() + "  where id = " + idal.ToString();;
                 int o = MySqlHelper.ExecuteNonQuery(_connection, sql);
                 
             }
             catch(MySqlException ex)
             {
                 throw ex;
                  
             }
             finally
             {
                 _connection.Close();
             }
         }


        public System.Data.DataTable GetAlimentiByCategoria(int idCat)
        {
            string sql = @"SELECT id,descrizione,prezzo,quantit from alimento WHERE attivo =1 and id_categoria = " + idCat.ToString();
            return MySqlHelper.ExecuteDataset(_connection, sql).Tables[0];
        }

        public double TotGiornataActualIncasso()
        {
            try
            {
                _connection.Open();
                string sql = @" select sum(totalecomanda) from comanda
                                where DATE(comanda.`data`) = DATE(NOW())  ;";
                object o = MySqlHelper.ExecuteScalar(_connection, sql);
                if (o == null)
                    return 0;
                else return Convert.ToDouble(o);
            }
            catch
            {

                return 0.00;
            }
            finally
            {
                _connection.Close();
            }
        }


        public double TotGiornataActualNcoperti()
        {
            try
            {
                _connection.Open();
                string sql = @"         SELECT sum(n_coperti) FROM comanda
                                        WHERE DATE(comanda.`data`) = DATE(NOW())   ;";
                object o = MySqlHelper.ExecuteScalar(_connection, sql);
                if (o == null)
                    return 0;
                else return Convert.ToDouble(o);
            }
            catch
            {

                return 0.00;
            }
            finally
            {
                _connection.Close();
            }
        }



        public int GetMaxComanda()
        {
            try
            {
                _connection.Open();
                string sql = @" select max(comanda.id_comanda) from comanda;";
                object o = MySqlHelper.ExecuteScalar(_connection, sql);
                if (o == null)
                    return 0;
                else return Convert.ToInt32(o);
            }
            catch
            {

                return 0;
            }
            finally {
                _connection.Close();
            }

        }

        public bool AddComanda(ComandaPrinc mycomanda, List<ComandaItem> items) {
            return AddComanda(mycomanda, items,0);
        }

        public   bool AddComanda(ComandaPrinc mycomanda, List<ComandaItem> items, double sconto)
        {
            string sqlMaster = @"INSERT INTO comanda (data,n_coperti,totalecomanda,sconto) VALUES(?datam,?coperti,?ttcom,?pconto);
                                  SELECT LAST_INSERT_ID();";

               string sqlItemOriginal = @"INSERT INTO comanda_alimento (id_comanda,id_alimento,qta,prezzo) VALUES(?idc,?ida,?qta,?przz);
                                  ";

               string sql_UpdateAlimentoqta = @"UPDATE alimento set quantit = ?qtaDisp where id =?idAl";

            string sql=string.Empty;
     
            int _privcomanda;
             

            _connection.Open();
            MySqlCommand myCommand = _connection.CreateCommand();
            MySqlCommand myCommandItem = _connection.CreateCommand();
            MySqlCommand myCommandUpdateComanda = _connection.CreateCommand();

            MySqlTransaction myTrans;
            myTrans = _connection.BeginTransaction();
            myCommandUpdateComanda.Connection =   myCommandItem.Connection=  myCommand.Connection = _connection;
            myCommandUpdateComanda.Transaction =     myCommandItem.Transaction =  myCommand.Transaction = myTrans;
            try
            {
                myCommand.CommandText = sqlMaster;
                myCommand.Parameters.AddWithValue("?datam", mycomanda.dataarr);
                myCommand.Parameters.AddWithValue("?coperti", mycomanda.ncoperti);
                myCommand.Parameters.AddWithValue("?ttcom", mycomanda.totPriceComanda);
                myCommand.Parameters.AddWithValue("?pconto", sconto); 
             _privcomanda =  Convert.ToInt32(  myCommand.ExecuteScalar()) ;

            
                foreach(ComandaItem joi in items)
                {
                    myCommandItem.CommandText = sqlItemOriginal;
                    myCommandItem.Parameters.AddWithValue("?idc", _privcomanda);
                    myCommandItem.Parameters.AddWithValue("?ida", joi.IdAlimento);
                    myCommandItem.Parameters.AddWithValue("?qta", joi.Quant);
                    myCommandItem.Parameters.AddWithValue("?przz", joi.totPrice);
                    myCommandItem.ExecuteNonQuery();
                    myCommandItem.Parameters.Clear();

                    myCommandUpdateComanda.CommandText = sql_UpdateAlimentoqta;
                    myCommandUpdateComanda.Parameters.AddWithValue("?qtaDisp", joi.quantRimasta);
                    myCommandUpdateComanda.Parameters.AddWithValue("?idAl", joi.IdAlimento);
                    myCommandUpdateComanda.ExecuteNonQuery();
                    myCommandUpdateComanda.Parameters.Clear();

                }
                  
                
               
                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                    myTrans.Rollback();
                  
                }
                catch (MySqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        //Console.WriteLine("An exception of type " + ex.GetType() +
                        //                  " was encountered while attempting to roll back the transaction.");
                    }
                }
                return false; 
            }
            finally
            {
                _connection.Close();
            }

            

        }

        public double PriceCoperto()
        {
            try
            {
                _connection.Open();
                string sql = @"         SELECT prezzo FROM alimento
                                        WHERE  id_categoria=10   ;";
                object o = MySqlHelper.ExecuteScalar(_connection, sql);
                if (o == null)
                    return 0;
                else return Convert.ToDouble(o);
            }
            catch
            {

                return 0.00;
            }
            finally
            {
                _connection.Close();
            }
        }

     

          

    }
}
