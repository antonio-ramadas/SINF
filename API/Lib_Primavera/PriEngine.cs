using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.ErpBS900;         // Use Primavera interop's [Path em C:\Program Files\Common Files\PRIMAVERA\SG800]
using Interop.StdPlatBS900;
using Interop.StdBE900;
using ADODB;
using System.Data.SqlClient;

namespace SFA_REST.Lib_Primavera
{
    public class PriEngine
    {
        //Business Service - BS
        public static StdPlatBS Platform { get; set; }
        public static ErpBS Engine { get; set; }
        public static SqlConnection SupportDB { get; set; }


        public static bool InitializeCompany(string Company, string User, string Password)
        {
            //obtem serviçoes e regras de negocio sobre a configuração do módulo
            StdBSConfApl objAplConf = new StdBSConfApl();
            //proxy dos servidores da plataforma
            StdPlatBS Plataforma = new StdPlatBS();
            //acesso aos serviços de negócio de um modulo
            ErpBS MotorLE = new ErpBS();

            EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
            objTipoPlataforma = EnumTipoPlataforma.tpProfissional;

            objAplConf.Instancia = "Default";
            objAplConf.AbvtApl = "GCP";
            objAplConf.PwdUtilizador = Password;
            objAplConf.Utilizador = User;
            objAplConf.LicVersaoMinima = "9.00";


            StdBETransaccao objStdTransac = new StdBETransaccao();

            // Opem platform.
            try
            {
                Plataforma.AbrePlataformaEmpresa(ref Company, ref objStdTransac, ref objAplConf, ref objTipoPlataforma, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error on open Primavera Platform.");
            }

            // Is plt initialized?
            if (Plataforma.Inicializada)
            {

                // Retuns the ptl.
                Platform = Plataforma;

                bool blnModoPrimario = true;

                // Open Engine
                MotorLE.AbreEmpresaTrabalho(EnumTipoPlataforma.tpProfissional, ref Company, ref User, ref Password, ref objStdTransac, "Default", ref blnModoPrimario);
                MotorLE.set_CacheActiva(false);

                // Returns the engine.
                Engine = MotorLE;

                try
                {
                    SupportDB = new SqlConnection("server=.\\PRIMAVERA;uid=sa;pwd=Feup2014;database=Support;");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                return true;
            }
            else
            {
                return false;
            }


        }

        public static bool isOpen()
        {
            if (!Platform.Inicializada)
                InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim());

            return true;
        }


        public static bool AuthenticateUser(Model.User user)
        {
            SqlDataReader data = null;
            try
            {
                SupportDB.Open();
                string query = "SELECT Password FROM PasswordUtilizador WHERE Utilizador = '" + user.username + "'";
                SqlCommand com = new SqlCommand(query, SupportDB);
                SqlDataReader result = com.ExecuteReader();
                result.Read();
                if (user.password == result.GetString(0))
                    return true;
                else return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

            return false;
        }

    }

}
