using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using Interop.RhpBE900;
using ADODB;
using System.Globalization;

namespace SFA_REST.Lib_Primavera
{
    public class PriIntegration
    {

        #region Costumer

        public static List<Model.Customer> ListCustomers()
        { 
            StdBELista objList;

            List<Model.Customer> listCustomers = new List<Model.Customer>();
            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    string query = "SELECT Cliente, Nome, Fac_Tel, Fac_Mor as Morada, B2BEnderecoMail as Mail, CDU_DataNascimento, NumContrib as NIF, CDU_GruposDeClientes, CDU_Sexo, CDU_Nacionalidade FROM  CLIENTES";
                    objList = PriEngine.Engine.Consulta(query);

                    while (!objList.NoFim())
                    {
                        listCustomers.Add(new Model.Customer
                        {
                            id = objList.Valor("Cliente"),
                            name = objList.Valor("Nome"),
                            phoneNumber = objList.Valor("Fac_Tel"),
                            address = objList.Valor("Morada"),
                            email = objList.Valor("Mail"),
                            customerGroups = objList.Valor("CDU_GruposDeClientes"),
                            gender = objList.Valor("CDU_Sexo"),
                            dateOfBirth = objList.Valor("CDU_DataNascimento").ToString(),
                            nationality = objList.Valor("CDU_Nacionalidade"),
                            nif = objList.Valor("NIF")
                        });
                        objList.Seguinte();

                    }

                    return listCustomers;
                }
                else
                    return null;
            }
            catch (Exception e)
            { 
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
           
        }

        public static Lib_Primavera.Model.Customer GetCustomer(string id)
        {
            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(id))
                {
                    string query = "SELECT Cliente, Nome, Fac_Tel, Fac_Mor as Morada, B2BEnderecoMail as Mail, CDU_DataNascimento, NumContrib as NIF, CDU_GruposDeClientes, CDU_Sexo, CDU_Nacionalidade FROM CLIENTES WHERE Cliente = '" + id + "'";
                    StdBELista objCli = PriEngine.Engine.Consulta(query);

                    if (!objCli.Vazia())
                    {
                        Model.Customer myCli;
                        myCli = new Model.Customer
                        {
                            id = objCli.Valor("Cliente"),
                            name = objCli.Valor("Nome"),
                            phoneNumber = objCli.Valor("Fac_Tel"),
                            address = objCli.Valor("Morada"),
                            email = objCli.Valor("Mail"),
                            customerGroups = objCli.Valor("CDU_GruposDeClientes"),
                            gender = objCli.Valor("CDU_Sexo"),
                            nationality = objCli.Valor("CDU_Nacionalidade"),
                            dateOfBirth = objCli.Valor("CDU_DataNascimento").ToString(),
                            nif = objCli.Valor("NIF")
                        };
                        return myCli;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public static Lib_Primavera.Model.ErrorResponse UpdateCustomer(String id, Lib_Primavera.Model.Customer customer)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBECliente objCli = new GcpBECliente();

            try {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (!PriEngine.Engine.Comercial.Clientes.Existe(id)) {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(id);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(customer.name);
                        objCli.set_Morada(customer.address);
                        objCli.set_B2BEnderecoMail(customer.email);
                        objCli.set_Telefone(customer.phoneNumber);
                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(id, "CDU_GruposDeClientes", customer.customerGroups);
                        PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(id, "CDU_Sexo", customer.gender);
                        PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(id, "CDU_Nacionalidade", customer.nationality);
                        PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(id, "CDU_DataNascimento", Convert.ToDateTime(customer.dateOfBirth));
                        objCli.set_NumContribuinte(customer.nif);


                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.ErrorResponse CreateCustomer(Model.Customer customer)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            GcpBECliente myCli = new GcpBECliente();

            try {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    myCli.set_Cliente(customer.id);
                    myCli.set_Nome(customer.name);
                    myCli.set_Morada(customer.address);
                    myCli.set_Telefone(customer.phoneNumber);
                    myCli.set_B2BEnderecoMail(customer.email);
                    myCli.set_NumContribuinte(customer.nif);
                    myCli.set_Moeda("EUR");
                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(customer.id, "CDU_GruposDeClientes", customer.customerGroups);
                    PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(customer.id, "CDU_Sexo", customer.gender);
                    PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(customer.id, "CDU_Nacionalidade", customer.nationality);
                    PriEngine.Engine.Comercial.Clientes.ActualizaValorAtributo(customer.id, "CDU_DataNascimento", customer.dateOfBirth);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Error Accessing the Company";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = "Missing or Incorrect field";
                return erro;
            }


        }

       

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Product

        public static Lib_Primavera.Model.Product GetProduct(string productId)
        {
            string CURRENCY = "EUR";
            string UNIT = "UN";

            GcpBEArtigoArmazens warehouses;
            
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            GcpBEArtigoMoeda objArtigoMoeda = new GcpBEArtigoMoeda();
            Model.Product myProd = new Model.Product();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {

                
                if (PriEngine.Engine.Comercial.Artigos.Existe(productId) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(productId);
                    myProd.id = objArtigo.get_Artigo();
                    myProd.description = objArtigo.get_Descricao();
                    myProd.model = objArtigo.get_Modelo();
                    myProd.brand = objArtigo.get_Marca();
                    myProd.vat = float.Parse(objArtigo.get_IVA(), CultureInfo.InvariantCulture.NumberFormat);
                    
                    //GET PRODUCT PRICE
                    if (PriEngine.Engine.Comercial.ArtigosPrecos.Existe(productId, CURRENCY, UNIT)==false)
                    {
                        myProd.price = float.MaxValue;
                    }else{
                        objArtigoMoeda = PriEngine.Engine.Comercial.ArtigosPrecos.Edita(productId, CURRENCY, UNIT);
                        myProd.price = objArtigoMoeda.get_PVP1();
                    }

                    myProd.quantity = PriEngine.Engine.Comercial.ArtigosArmazens.DaStockArtigo(productId);
                    myProd.warehouses = new List<string>();

                    warehouses = PriEngine.Engine.Comercial.ArtigosArmazens.ListaArtigosArmazens(productId);
                    foreach (GcpBEArtigoArmazem warehouse in warehouses)
                    {
                        myProd.warehouses.Add(warehouse.get_Armazem());
                    }

                    return myProd;
                }
                
            }
            else
            {
                return null;
            }

        }

        public static List<Model.Product> ListaArtigos()
        {
                        
            StdBELista objList;

            Model.Product art = new Model.Product();
            List<Model.Product> listArts = new List<Model.Product>();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Product();
                    //art.CodArtigo = objList.Valor("artigo");
                    //art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo


        #region SalesRepresentative

        public static List<Model.SalesRepresentative> listSalesRepresentatives()
        {
            StdBELista obj;

            List<Model.SalesRepresentative> listSalesRepresentative = new List<Model.SalesRepresentative>();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {
                string query = "SELECT Codigo, Email, Nome, DataNascimento, Morada, Pais, Nacionalidade, Telemovel, EstadoCivil, NumBI, NumContr, Sexo, CDU_Ativo FROM FUNCIONARIOS";
                obj = PriEngine.Engine.Consulta(query);

                while (!obj.NoFim())
                {
                    listSalesRepresentative.Add(new Model.SalesRepresentative
                    {
                        id = obj.Valor("Codigo"),
                        email = obj.Valor("Email"),
                        name = obj.Valor("Nome"),
                        dateOfBirth = obj.Valor("DataNascimento").ToString(),
                        address = obj.Valor("Morada"),
                        country = obj.Valor("Pais"),
                        nationality = obj.Valor("Nacionalidade"),
                        phoneNumber = obj.Valor("Telemovel"),
                        maritalStatus = obj.Valor("EstadoCivil"),
                        civilID = obj.Valor("NumBI"),
                        nif = obj.Valor("NumContr"),
                        gender = obj.Valor("Sexo"),
                        active = obj.Valor("CDU_Ativo").ToString()
                    });
                    obj.Seguinte();

                }

                return listSalesRepresentative;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.SalesRepresentative GetSalesRepresentative(string id)
        {
            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.RecursosHumanos.Funcionarios.Existe(id))
                    {
                        string query = "SELECT Codigo, Email, Nome, DataNascimento, Morada, Pais, Nacionalidade, Telemovel, EstadoCivil, NumBI, NumContr, Sexo, CDU_Ativo FROM FUNCIONARIOS WHERE Codigo = '" + id + "'";
                        StdBELista obj = PriEngine.Engine.Consulta(query);

                        if (!obj.Vazia())
                        {
                            Model.SalesRepresentative mySalesRep;
                            mySalesRep = new Model.SalesRepresentative
                            {
                                id = obj.Valor("Codigo"),
                                email = obj.Valor("Email"),
                                name = obj.Valor("Nome"),
                                dateOfBirth = obj.Valor("DataNascimento").ToString(),
                                address = obj.Valor("Morada"),
                                country = obj.Valor("Pais"),
                                nationality = obj.Valor("Nacionalidade"),
                                phoneNumber = obj.Valor("Telemovel"),
                                maritalStatus = obj.Valor("EstadoCivil"),
                                civilID = obj.Valor("NumBI"),
                                nif = obj.Valor("NumContr"),
                                gender = obj.Valor("Sexo"),
                                active = obj.Valor("CDU_Ativo").ToString()
                            };
                            return mySalesRep;
                        }
                        return null;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        // NOT FUNCTIONAL
        public static Lib_Primavera.Model.ErrorResponse CreateSalesRepresentative(Model.SalesRepresentative salesRepresentative)
        {
            System.Diagnostics.Debug.WriteLine("entrou na funcao de criaçao");
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            RhpBEFuncionario mySalesRep = new RhpBEFuncionario();

            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Entrou na criaçao");

                        mySalesRep.set_Funcionario(salesRepresentative.id);
                        mySalesRep.set_Email(salesRepresentative.email);
                        mySalesRep.set_Nome(salesRepresentative.name);
                        mySalesRep.set_TipoRendimento("A"); //Trabalho Dependente
                        mySalesRep.set_DataAdmissao(DateTime.Now);
                        mySalesRep.set_SegurancaSocial("001");
                        mySalesRep.set_Situacao("001"); //Efetivo
                        mySalesRep.set_Instrumento("001"); //Instrumento de Regulamentação do Trabalho
                        mySalesRep.set_Periodo("P01"); //Mensal
                        mySalesRep.set_Estabelecimento("001"); //Sede
                        mySalesRep.set_FormaPagSF("001"); //Tranche Completa-Mês Subsídio
                        mySalesRep.set_FormaPagSN("001"); //Tranche Completa-Mês Subsídio
                        /*mySalesRep.set_DataNascimento(Convert.ToDateTime(salesRepresentative.dateOfBirth));
                        mySalesRep.set_Morada(salesRepresentative.address);
                        mySalesRep.set_Pais(salesRepresentative.country);
                        mySalesRep.set_Nacionalidade(salesRepresentative.nationality);
                        mySalesRep.set_Telemovel(salesRepresentative.phoneNumber);
                        mySalesRep.set_EstadoCivil(salesRepresentative.maritalStatus);
                        mySalesRep.set_NumeroBI(salesRepresentative.civilID);
                        mySalesRep.set_NumContribuinte(salesRepresentative.nif);
                        mySalesRep.set_Sexo(salesRepresentative.gender);*/
                    //    mySalesRep.set_Moeda("EUR");

                        System.Diagnostics.Debug.WriteLine("Definiu tudo");
                    
                        PriEngine.Engine.RecursosHumanos.Funcionarios.Actualiza(mySalesRep);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                        System.Diagnostics.Debug.WriteLine(e.StackTrace);
                        erro.Erro = 1;
                        erro.Descricao = "Erro";
                        return erro;
                    }
                    
                    System.Diagnostics.Debug.WriteLine("Inseriu tudo");

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Error Accessing the Company";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = "Missing or Incorrect field";
                return erro;
            }


        }

        public static Lib_Primavera.Model.ErrorResponse DeactivateSalesRepresentative(string id)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            RhpBEFuncionario obj = new RhpBEFuncionario();

            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (!PriEngine.Engine.RecursosHumanos.Funcionarios.Existe(id))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "The Sales Representative doesn't exist";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.RecursosHumanos.Funcionarios.ActualizaValorAtributo(id, "CDU_Ativo", 0);

                        erro.Erro = 0;
                        erro.Descricao = "Success";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Error opening the commpany";
                    return erro;

                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        #endregion SalesRepresentative


        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {
                
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;
                    
                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }

                
        public static Model.ErrorResponse VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            

            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra


        #region DocsVenda

        public static Model.ErrorResponse Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();
             
            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();
             
            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();
            
            try
            {
                if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                   // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

     

        public static List<Model.DocVenda> Encomendas_List()
        {
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


       

        public static Model.DocVenda Encomenda_Get(string numdoc)
        {
            
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(SFA_REST.Properties.Settings.Default.Company.Trim(), SFA_REST.Properties.Settings.Default.User.Trim(), SFA_REST.Properties.Settings.Default.Password.Trim()) == true)
            {
                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocsVenda
    }
}