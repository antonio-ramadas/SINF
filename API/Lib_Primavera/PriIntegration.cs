using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using Interop.RhpBE900;
using Interop.CrmBE900;
using ADODB;
using System.Globalization;

namespace SFA_REST.Lib_Primavera
{
    public class PriIntegration
    {

        #region Customer

        public static List<Model.Customer> ListCustomers()
        { 
            List<Model.Customer> listCustomers = new List<Model.Customer>();
            try
            {
                if (PriEngine.isOpen())
                {

                    string query = "SELECT Cliente, Nome, Fac_Tel, Fac_Mor as Morada, B2BEnderecoMail as Mail, NumContrib as NIF, Pais, CDU_CampoVar1, CDU_CampoVar2, CDU_CampoVar3 FROM  CLIENTES";
                    StdBELista objList = PriEngine.Engine.Consulta(query);
                    List<string> labelsList;
                    while (!objList.NoFim())
                    {
                        labelsList = new List<string>();
                        labelsList.Add(objList.Valor("CDU_CampoVar1"));
                        labelsList.Add(objList.Valor("CDU_CampoVar2"));
                        labelsList.Add(objList.Valor("CDU_CampoVar3"));
                        listCustomers.Add(new Model.Customer
                        {
                            id = objList.Valor("Cliente"),
                            name = objList.Valor("Nome"),
                            phoneNumber = objList.Valor("Fac_Tel"),
                            address = objList.Valor("Morada"),
                            email = objList.Valor("Mail"),
                            nationality = objList.Valor("Pais"),
                            nif = objList.Valor("NIF"),
                            labels = labelsList
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
            if (PriEngine.isOpen())
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(id))
                {
                    string query = "SELECT Cliente, Nome, Fac_Tel, Fac_Mor as Morada, B2BEnderecoMail as Mail, NumContrib as NIF, Pais, Notas, CDU_CampoVar1, CDU_CampoVar2, CDU_CampoVar3 FROM CLIENTES WHERE Cliente = '" + id + "'";
                    StdBELista objCli = PriEngine.Engine.Consulta(query);
                    List<string> labelsList;

                    if (!objCli.Vazia())
                    {
                        Model.Customer myCli;
                        labelsList = new List<string>();
                        labelsList.Add(objCli.Valor("CDU_CampoVar1"));
                        labelsList.Add(objCli.Valor("CDU_CampoVar2"));
                        labelsList.Add(objCli.Valor("CDU_CampoVar3"));
                        myCli = new Model.Customer
                        {
                            id = objCli.Valor("Cliente"),
                            name = objCli.Valor("Nome"),
                            phoneNumber = objCli.Valor("Fac_Tel"),
                            address = objCli.Valor("Morada"),
                            email = objCli.Valor("Mail"),
                            nationality = objCli.Valor("Pais"),
                            nif = objCli.Valor("NIF"),
                            notes = objCli.Valor("Notas"),
                            labels = labelsList
                        };
                        return myCli;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public static List<Model.Customer> GetCustomerByName(string name)
        {
            List<Model.Customer> listCustomers = new List<Model.Customer>();
            try
            {
                if (PriEngine.isOpen())
                {
                    string query = "SELECT DISTINCT Cliente, Nome FROM CLIENTES WHERE Nome LIKE '%" + name + "%' OR Cliente LIKE '%" + name + "%' ORDER BY Cliente";
                    StdBELista objList = PriEngine.Engine.Consulta(query);

                    while (!objList.NoFim())
                    {
                        listCustomers.Add(new Model.Customer
                        {
                            id = objList.Valor("Cliente"),
                            name = objList.Valor("Nome")
                        });
                        objList.Seguinte();
                    }

                    return listCustomers;

                }
                return listCustomers;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static List<Model.Customer> GetTopCustomers(string number)
        {
            List<Model.Customer> listCustomers = new List<Model.Customer>();
            try
            {
                if (PriEngine.isOpen())
                {
                    string query = "SELECT TOP " + number + " Cliente, Nome, s.Number FROM CLIENTES INNER JOIN (SELECT Entidade, count(*) as Number FROM CabecDoc GROUP BY Entidade) s ON s.Entidade = Cliente ORDER BY s.Number DESC";
                    StdBELista objList = PriEngine.Engine.Consulta(query);

                    while (!objList.NoFim())
                    {
                        listCustomers.Add(new Model.Customer
                        {
                            id = objList.Valor("Cliente"),
                            name = objList.Valor("Nome")
                        });
                        objList.Seguinte();
                    }

                    return listCustomers;

                }
                return listCustomers;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static Lib_Primavera.Model.ErrorResponse UpdateCustomer(String id, Lib_Primavera.Model.Customer customer)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBECliente objCli = new GcpBECliente();

            try {
                if (PriEngine.isOpen())
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
                        objCli.set_NumContribuinte(customer.nif);
                        objCli.set_Pais(customer.nationality);
                        objCli.set_Observacoes(customer.notes);
                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

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

        public static Lib_Primavera.Model.ErrorResponse UpdateCustomerNotes(String id, Lib_Primavera.Model.Customer customer)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBECliente objCli = new GcpBECliente();

            try
            {
                if (PriEngine.isOpen())
                {
                    if (!PriEngine.Engine.Comercial.Clientes.Existe(id))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(id);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Observacoes(customer.notes);
                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

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
                if (PriEngine.isOpen())
                {
                    myCli.set_Cliente(customer.id);
                    myCli.set_Nome(customer.name);
                    myCli.set_Morada(customer.address);
                    myCli.set_Telefone(customer.phoneNumber);
                    myCli.set_B2BEnderecoMail(customer.email);
                    myCli.set_NumContribuinte(customer.nif);
                    myCli.set_Pais(customer.nationality);
                    myCli.set_Moeda("EUR");
                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

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

        #endregion Customer;   // -----------------------------  END   CLIENTE    -----------------------


        #region Product

        public static List<Model.Product> ListProducts()
        {
            StdBELista objList;

            List<Model.Product> listArts = new List<Model.Product>();
            if (PriEngine.isOpen())
            {
                string query = "SELECT * FROM ARTIGO";
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    listArts.Add(new Model.Product
                    {
                        id = objList.Valor("Artigo"),
                        description = objList.Valor("Descricao"),
                        quantity = objList.Valor("STKActual"),
                        brand = objList.Valor("Marca"),
                        model = objList.Valor("Modelo"),
                        category = objList.Valor("Familia"),
                        subCategory = objList.Valor("SubFamilia")
                    });
                    objList.Seguinte();
                }

                return listArts;
            }
            else
                return null;
        }

        public static List<Model.Product> ListProductsByHint(string hint)
        {
            StdBELista objList;

            List<Model.Product> listArts = new List<Model.Product>();
            if (PriEngine.isOpen())
            {
                string query = "SELECT TOP 10 ARTIGO.Artigo, ARTIGO.Descricao, ARTIGO.STKActual, ARTIGO.Marca, ARTIGO.Modelo, ARTIGO.Familia, ARTIGO.SubFamilia FROM ARTIGO, Familias WHERE Familias.Familia = artigo.familia AND Familias.Descricao LIKE '%" + hint + "%'";
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    listArts.Add(new Model.Product
                    {
                        id = objList.Valor("Artigo"),
                        description = objList.Valor("Descricao"),
                        quantity = objList.Valor("STKActual"),
                        brand = objList.Valor("Marca"),
                        model = objList.Valor("Modelo"),
                        category = objList.Valor("Familia"),
                        subCategory = objList.Valor("SubFamilia")
                    });
                    objList.Seguinte();
                }

                return listArts;
            }
            else
                return null;
        }

        public static List<Model.Product> GetProductsByCategory(string category)
        {
            StdBELista objList;

            List<Model.Product> listArts = new List<Model.Product>();
            if (PriEngine.isOpen())
            {
                string query = "SELECT * FROM ARTIGO WHERE Familia = '" + category + "'";
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    listArts.Add(new Model.Product
                    {
                        id = objList.Valor("Artigo"),
                        description = objList.Valor("Descricao"),
                        quantity = objList.Valor("STKActual"),
                        brand = objList.Valor("Marca"),
                        model = objList.Valor("Modelo"),
                        category = objList.Valor("Familia"),
                        subCategory = objList.Valor("SubFamilia")
                    });
                    objList.Seguinte();
                }

                return listArts;
            }
            else
                return null;
        }

        public static List<Model.Product> GetProductsBySubCategory(string subCategory)
        {
            StdBELista objList;

            List<Model.Product> listArts = new List<Model.Product>();
            if (PriEngine.isOpen())
            {
                string query = "SELECT * FROM ARTIGO WHERE SubFamilia = '" + subCategory + "'";
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    listArts.Add(new Model.Product
                    {
                        id = objList.Valor("Artigo"),
                        description = objList.Valor("Descricao"),
                        quantity = objList.Valor("STKActual"),
                        brand = objList.Valor("Marca"),
                        model = objList.Valor("Modelo"),
                        category = objList.Valor("Familia"),
                        subCategory = objList.Valor("SubFamilia")
                    });
                    objList.Seguinte();
                }

                return listArts;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Product GetProduct(string productId)
        {
            string CURRENCY = "EUR";
            string UNIT = "UN";

            GcpBEArtigoArmazens warehouses;
            
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            GcpBEArtigoMoeda objArtigoMoeda = new GcpBEArtigoMoeda();
            Model.Product myProd = new Model.Product();

            if (PriEngine.isOpen())
            {
                if (PriEngine.Engine.Comercial.Artigos.Existe(productId) == false)
                    return null;
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(productId);
                    myProd.id = objArtigo.get_Artigo();
                    myProd.description = objArtigo.get_Descricao();
                    myProd.model = objArtigo.get_Modelo();
                    myProd.brand = objArtigo.get_Marca();
                    myProd.category = objArtigo.get_Familia();
                    myProd.subCategory = objArtigo.get_SubFamilia();
                    myProd.vat = float.Parse(objArtigo.get_IVA(), CultureInfo.InvariantCulture.NumberFormat);

                    myProd.salesCount = GetSalesCount(productId);

                    //GET PRODUCT PRICE
                    if (PriEngine.Engine.Comercial.ArtigosPrecos.Existe(productId, CURRENCY, UNIT)==false)
                    {
                        myProd.price = float.MaxValue;
                    }
                    else
                    {
                        objArtigoMoeda = PriEngine.Engine.Comercial.ArtigosPrecos.Edita(productId, CURRENCY, UNIT);
                        myProd.price = objArtigoMoeda.get_PVP1();
                    }

                    myProd.quantity = PriEngine.Engine.Comercial.ArtigosArmazens.DaStockArtigo(productId);
                    
                    //Get warehouse's list
                    myProd.warehouses = new List<Lib_Primavera.Model.WarehouseProduct>();
                    warehouses = PriEngine.Engine.Comercial.ArtigosArmazens.ListaArtigosArmazens(productId);
                    Lib_Primavera.Model.WarehouseProduct myWarehouse;
                    foreach (GcpBEArtigoArmazem warehouse in warehouses)
                    {
                        myWarehouse = new Lib_Primavera.Model.WarehouseProduct();
                        myWarehouse.id = warehouse.get_Armazem();
                        myWarehouse.quantity = warehouse.get_StkActual();
                        myProd.warehouses.Add(myWarehouse);
                    }

                    string query = "SELECT CDU_CampoVar3 AS Image FROM ARTIGO WHERE Artigo ='" + myProd.id + "'";
                    StdBELista list = PriEngine.Engine.Consulta(query);
                    
                    if(!list.Vazia())
                        myProd.imageURL = list.Valor("Image");
                        
                    return myProd;
                }
                
            }
            else
            {
                return null;
            }

        }

        public static List<Model.Product> GetTopProducts(string number)
        {
            StdBELista objList;

            List<Model.Product> listArts = new List<Model.Product>();
            if (PriEngine.isOpen())
            {
                string query = "SELECT TOP " + number + " Artigo, Descricao, s.Number FROM ARTIGO INNER JOIN (SELECT Artigo AS Code, count(*) as Number FROM LinhasDoc GROUP BY Artigo) s ON s.Code = Artigo ORDER BY s.Number DESC";
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    listArts.Add(new Model.Product
                    {
                        id = objList.Valor("Artigo"),
                        description = objList.Valor("Descricao")
                    });
                    objList.Seguinte();
                }

                return listArts;
            }
            else
                return null;
        }

        public static double GetSalesCount(string productId)
        {
            StdBELista objList;
            if(PriEngine.isOpen())
            {
                if (PriEngine.Engine.Comercial.Artigos.Existe(productId) == false)
                {
                    return 0;
                }

                string query = "SELECT SUM(Quantidade) as salesCount FROM LinhasDoc WHERE Artigo ='" + productId + "'";
                objList = PriEngine.Engine.Consulta(query);

                return objList.Valor("salesCount");
                
            }
            else
            {
                return 0;
            }
        }

        #endregion Product


        #region Category

        public static List<Model.Category> CategoryList()
        {

            StdBELista list, subList; 

            List<Model.Category> listArts = new List<Model.Category>();
            if (PriEngine.isOpen() == true)
            {

                string query = "SELECT * FROM Familias";
                list = PriEngine.Engine.Consulta(query);

                while (!list.NoFim())
                {
                    string subQuery = "SELECT * FROM SubFamilias WHERE Familia='" + list.Valor("Familia") + "'";
                    subList = PriEngine.Engine.Consulta(subQuery);
                    List<Model.Category> subFamilias = new List<Model.Category>();
                    while (!subList.NoFim())
                    {
                        subFamilias.Add(new Model.Category
                        {
                            family = subList.Valor("SubFamilia"),
                            description = subList.Valor("Descricao"),
                            children = null
                        });

                        subList.Seguinte();
                    }

                    listArts.Add(new Model.Category
                    {
                        family = list.Valor("Familia"),
                        description = list.Valor("Descricao"),
                        children = subFamilias
                    });
                    list.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Category


        #region SalesRepresentative

        public static List<Model.SalesRepresentative> listSalesRepresentatives()
        {
            StdBELista obj;

            List<Model.SalesRepresentative> listSalesRepresentative = new List<Model.SalesRepresentative>();

            if (PriEngine.isOpen() == true)
            {
                string query = "SELECT * FROM VENDEDORES";
                obj = PriEngine.Engine.Consulta(query);

                while (!obj.NoFim())
                {
                    listSalesRepresentative.Add(new Model.SalesRepresentative
                    {
                        id = obj.Valor("Vendedor"),
                        email = obj.Valor("EMail"),
                        name = obj.Valor("Nome"),
                        address = obj.Valor("Morada"),
                        phoneNumber = obj.Valor("Telefone"),
                        active = obj.Valor("DisponivelEmPMS").ToString()
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
                if (PriEngine.isOpen() == true)
                {
                    if (PriEngine.Engine.Comercial.Vendedores.Existe(id))
                    {
                        string query = "SELECT * FROM VENDEDORES WHERE Vendedor = '" + id + "'";
                        StdBELista obj = PriEngine.Engine.Consulta(query);

                        if (!obj.Vazia())
                        {
                            Model.SalesRepresentative mySalesRep;
                            mySalesRep = new Model.SalesRepresentative
                            {
                                id = obj.Valor("Vendedor"),
                                email = obj.Valor("EMail"),
                                name = obj.Valor("Nome"),
                                address = obj.Valor("Morada"),
                                phoneNumber = obj.Valor("Telefone"),
                                active = obj.Valor("DisponivelEmPMS").ToString()
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

        public static List<Model.Customer> GetTopCustomersBySalesRepresentative(string id, string number)
        {
            List<Model.Customer> listCustomers = new List<Model.Customer>();
            try
            {
                if (PriEngine.isOpen())
                {

                    string query = "SELECT TOP " + number + " Cliente, Nome, s.Number FROM CLIENTES INNER JOIN (SELECT Entidade, count(*) as Number FROM CabecDoc WHERE CabecDoc.Responsavel = '" + id + "' GROUP BY Entidade) s ON s.Entidade = Cliente ORDER BY s.Number DESC";
                    StdBELista objList = PriEngine.Engine.Consulta(query);
                    while (!objList.NoFim())
                    {
                        listCustomers.Add(new Model.Customer
                        {
                            id = objList.Valor("Cliente"),
                            name = objList.Valor("Nome")
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

        public static Lib_Primavera.Model.ErrorResponse CreateSalesRepresentative(Model.SalesRepresentative salesRepresentative)
        {
            System.Diagnostics.Debug.WriteLine("entrou na funcao de criaçao");
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            GcpBEVendedor mySalesRep = new GcpBEVendedor();

            try
            {
                if (PriEngine.isOpen() == true)
                {
                    try
                    {
                        mySalesRep.set_Vendedor(salesRepresentative.id);
                        mySalesRep.set_Email(salesRepresentative.email);
                        mySalesRep.set_Nome(salesRepresentative.name);
                        mySalesRep.set_Morada(salesRepresentative.address);
                        mySalesRep.set_Telefone(salesRepresentative.phoneNumber);
                    
                        PriEngine.Engine.Comercial.Vendedores.Actualiza(mySalesRep);
                       
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
            GcpBEVendedor obj = new GcpBEVendedor();

            try
            {
                if (PriEngine.isOpen() == true)
                {
                    if (!PriEngine.Engine.Comercial.Vendedores.Existe(id))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "The Sales Representative doesn't exist";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.Comercial.Vendedores.ActualizaValorAtributo(id, "DisponivelEmPMS", 0);

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


        #region Customervisits

        public static List<Model.Visits> ListVisits()
        {
            StdBELista obj;

            List<Model.Visits> listvisits = new List<Model.Visits>();
            try
            {
                if (PriEngine.isOpen())
                {

                    string query = "SELECT * from Tarefas";
                    obj = PriEngine.Engine.Consulta(query);

                    while (!obj.NoFim())
                    {
                        listvisits.Add(new Model.Visits
                        {
                            id = obj.Valor("Id"),
                            customerID = obj.Valor("EntidadePrincipal"),
                            representativeID = obj.Valor("Utilizador"),
                            date = obj.Valor("DataInicio").tostring(),
                            summary = obj.Valor("Resumo"),
                            notes = obj.Valor("Descricao")
                        });
                        obj.Seguinte();
                    }

                    return listvisits;
                }
                else
                    return null;
            }
            catch (Exception e){
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static Lib_Primavera.Model.Visits GetVisit(string id)
        {
            if (PriEngine.isOpen() == true)
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(id))
                {
                    string query = "select * from Tarefas where Id = '" + id + "'";
                    StdBELista obj = PriEngine.Engine.Consulta(query);

                    if (!obj.Vazia())
                    {
                        Model.Visits myVisits;
                        myVisits = new Model.Visits
                        {
                            id = obj.Valor("Id"),
                            customerID = obj.Valor("EntidadePrincipal"),
                            representativeID = obj.Valor("Utilizador"),
                            date = obj.Valor("DataInicio").tostring(),
                            summary = obj.Valor("Resumo"),
                            notes = obj.Valor("Descricao")
                        };
                        return myVisits;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public static Lib_Primavera.Model.ErrorResponse CreateVisit(Model.Visits visit)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            CrmBEActividade myVisit = new CrmBEActividade();

            try
            {
                if (PriEngine.isOpen() == true)
                {
                    myVisit.set_ID(visit.id);
                    myVisit.set_IDTipoActividade("9f832b71-08cf-4b4d-a31a-aa9c834e058e");
                    myVisit.set_EntidadePrincipal(visit.customerID);
                    myVisit.set_Utilizador(visit.representativeID);
                    myVisit.set_DataInicio(Convert.ToDateTime(visit.date));
                    myVisit.set_DataFim(Convert.ToDateTime(visit.date));
                    myVisit.set_Resumo(visit.summary);

                    PriEngine.Engine.CRM.Actividades.Actualiza(myVisit);
                    erro.Erro = 0;
                    erro.Descricao = "sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "error accessing the company";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                erro.Erro = 1;
                erro.Descricao = "missing or incorrect field";
                return erro;
            }

        }

        #endregion Customervisits


        #region Leads

        public static List<Model.Lead> ListLeads()
        {
            StdBELista obj;

            List<Model.Lead> listLeads = new List<Model.Lead>();
            try
            {
                if (PriEngine.isOpen() == true)
                {

                    string query = "SELECT * FROM CabecOportunidadesVenda";
                    obj = PriEngine.Engine.Consulta(query);

                    while (!obj.NoFim())
                    {
                        listLeads.Add(new Model.Lead
                        {
                            id = ((obj.Valor("ID")).Replace("{", "")).Replace("}", ""),
                            customerID = obj.Valor("Entidade"),
                            expirationDate = obj.Valor("DataExpiracao").ToString(),
                            description = obj.Valor("Descricao"),
                            summary = obj.Valor("Resumo"),
                            value = obj.Valor("ValorTotalOV").ToString(),
                            salesRepID = obj.Valor("Vendedor"),
                            type = obj.Valor("Oportunidade")
                        });
                        obj.Seguinte();
                    }

                    return listLeads;
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

        public static List<Model.Lead> ListLeadsByCustomer(string customerId)
        {
            StdBELista obj;

            List<Model.Lead> listLeads = new List<Model.Lead>();
            try
            {
                if (PriEngine.isOpen() == true)
                {

                    string query = "SELECT * FROM CabecOportunidadesVenda WHERE Entidade = '"+customerId+"'";
                    obj = PriEngine.Engine.Consulta(query);

                    while (!obj.NoFim())
                    {
                        listLeads.Add(new Model.Lead
                        {
                            id = ((obj.Valor("ID")).Replace("{", "")).Replace("}", ""),
                            customerID = obj.Valor("Entidade"),
                            expirationDate = obj.Valor("DataExpiracao").ToString(),
                            description = obj.Valor("Descricao"),
                            summary = obj.Valor("Resumo"),
                            value = obj.Valor("ValorTotalOV").ToString(),
                            salesRepID = obj.Valor("Vendedor"),
                            type = obj.Valor("Oportunidade")
                        });
                        obj.Seguinte();
                    }

                    return listLeads;
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

        public static Lib_Primavera.Model.Lead GetLead(string id)
        {
            if (PriEngine.isOpen() == true)
            {
                System.Diagnostics.Debug.WriteLine("Abriu");

                string query = "SELECT * FROM CabecOportunidadesVenda WHERE ID = '" + id + "'";
                StdBELista obj = PriEngine.Engine.Consulta(query);

                if (!obj.Vazia())
                {
                    System.Diagnostics.Debug.WriteLine("Apanhou");
                    Model.Lead myLead;
                    myLead = new Model.Lead
                    {
                        id = ((obj.Valor("ID")).Replace("{", "")).Replace("}", ""),
                        customerID = obj.Valor("Entidade"),
                        expirationDate = obj.Valor("DataExpiracao").ToString(),
                        description = obj.Valor("Descricao"),
                        summary = obj.Valor("Resumo"),
                        value = obj.Valor("ValorTotalOV").ToString(),
                        salesRepID = obj.Valor("Vendedor"),
                        type = obj.Valor("Oportunidade")
                    };
                    return myLead;
                }
                return null;
            }
            return null;
        }

        public static Lib_Primavera.Model.ErrorResponse CreateLead(Model.Lead lead)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            CrmBEOportunidadeVenda myLead = new CrmBEOportunidadeVenda();

            try 
            {
                if (PriEngine.isOpen() == true)
                {
                    myLead.set_ID(Guid.NewGuid().ToString());
                    myLead.set_Oportunidade(lead.type);
                    myLead.set_Descricao(lead.description);
                    myLead.set_Entidade(lead.customerID);
                    myLead.set_TipoEntidade("C");
                    myLead.set_ValorTotalOV(Convert.ToDouble(lead.value));
                    myLead.set_Resumo(lead.summary);
                    myLead.set_Vendedor(lead.salesRepID);
                    myLead.set_CicloVenda("CV_HW");
                    myLead.set_ValorTotalOV(Convert.ToDouble(lead.value));
                    myLead.set_DataCriacao(DateTime.Now);
                    myLead.set_DataExpiracao(new DateTime(2100, 12, 12));
                    myLead.set_Moeda("EUR");

                    PriEngine.Engine.CRM.OportunidadesVenda.Actualiza(myLead);

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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                erro.Erro = 1;
                erro.Descricao = "Missing or Incorrect field";
                return erro;
            }
        }

        public static Lib_Primavera.Model.ErrorResponse DeleteLead(string id)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            string ID = "{" + id + "}";

            try
            {
                if (PriEngine.isOpen() == true)
                {
                    if(!PriEngine.Engine.CRM.OportunidadesVenda.ExisteID(ID))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "The Lead doesn't exist.";
                        return erro;
                    }
                    PriEngine.Engine.CRM.OportunidadesVenda.RemoveID(ID);

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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                erro.Erro = 1;
                erro.Descricao = "Missing or Incorrect field";
                return erro;
            }
        }

        #endregion Leads


        #region DocCompra

        public static List<Model.DocCompra> VGR_List()
        {
                
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.isOpen() == true)
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

            Interop.GcpBE900.PreencheRelacaoCompras rl = new Interop.GcpBE900.PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.isOpen() == true)
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


        #region SalesOrder

        public static Model.ErrorResponse CreateSalesOrder(Model.SalesOrder dv)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            Interop.GcpBE900.PreencheRelacaoVendas rl = new Interop.GcpBE900.PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.isOpen())
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.entity);
                    myEnc.set_Serie(dv.serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
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

        public static List<Model.SalesOrder> ListSalesOrder()
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.SalesOrder dv = new Model.SalesOrder();
            List<Model.SalesOrder> listdv = new List<Model.SalesOrder>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.isOpen() == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Responsavel, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.SalesOrder();
                    dv.id = objListCab.Valor("id");
                    dv.entity = objListCab.Valor("Entidade");
                    dv.numDoc = objListCab.Valor("NumDoc");
                    dv.date = objListCab.Valor("Data");
                    dv.totalMerc = objListCab.Valor("TotalMerc");
                    dv.serie = objListCab.Valor("Serie");
                    dv.salesRep = objListCab.Valor("Responsavel");
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

        public static Model.SalesOrder Encomenda_Get(string numdoc)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.SalesOrder dv = new Model.SalesOrder();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.isOpen())
            {
                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie, Responsavel From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.SalesOrder();
                dv.id = objListCab.Valor("id");
                dv.entity = objListCab.Valor("Entidade");
                dv.numDoc = objListCab.Valor("NumDoc");
                dv.date = objListCab.Valor("Data");
                dv.totalMerc = objListCab.Valor("TotalMerc");
                dv.serie = objListCab.Valor("Serie");
                dv.salesRep = objListCab.Valor("Responsavel");
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

        public static List<Model.SalesOrder> GetSalesOrderByRep(string salesRepId, string number)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.SalesOrder dv = new Model.SalesOrder();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();
            List<Model.SalesOrder> listSalesOrder = new List<Model.SalesOrder>();

            if (PriEngine.isOpen())
            {
                string st;
                if(Int32.Parse(number) < 0)
                    st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Responsavel='" + salesRepId + "' ORDER BY Data DESC";
                else st = "SELECT TOP " + number + " id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Responsavel='" + salesRepId + "' ORDER BY Data DESC";
                objListCab = PriEngine.Engine.Consulta(st);
                while (!objListCab.NoFim())
                {
                    dv = new Model.SalesOrder();
                    dv.id = objListCab.Valor("id");
                    dv.entity = objListCab.Valor("Entidade");
                    dv.numDoc = objListCab.Valor("NumDoc");
                    dv.date = objListCab.Valor("Data");
                    dv.totalMerc = objListCab.Valor("TotalMerc");
                    dv.serie = objListCab.Valor("Serie");
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
                    listSalesOrder.Add(dv);
                    objListCab.Seguinte();
                }

                return listSalesOrder;
            }
            return null;
        }

        public static List<Model.SalesOrder> GetSalesOrderByCustomer(string costumerId, string number)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.SalesOrder dv = new Model.SalesOrder();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();
            List<Model.SalesOrder> listSalesOrder = new List<Model.SalesOrder>();

            if (PriEngine.isOpen() == true)
            {
                string st;
                if (Int32.Parse(number) < 0)
                    st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Entidade='" + costumerId + "' ORDER BY Data DESC";
                else st = "SELECT TOP " + number + " id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Entidade='" + costumerId + "' ORDER BY Data DESC";
                objListCab = PriEngine.Engine.Consulta(st);
                while (!objListCab.NoFim())
                {
                    dv = new Model.SalesOrder();
                    dv.id = objListCab.Valor("id");
                    dv.entity = objListCab.Valor("Entidade");
                    dv.numDoc = objListCab.Valor("NumDoc");
                    dv.date = objListCab.Valor("Data");
                    dv.totalMerc = objListCab.Valor("TotalMerc");
                    dv.serie = objListCab.Valor("Serie");
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
                    listSalesOrder.Add(dv);
                    objListCab.Seguinte();
                }
                return listSalesOrder;
            }
            return null;
        }

        public static List<Model.SalesOrderHistory> GetSalesOrderByProductForHistory(string productID, string number)
        {
            StdBELista objListLinhas;
            List<Model.SalesOrderHistory> listSalesOrder = new List<Model.SalesOrderHistory>();
            try
            {
                if (PriEngine.isOpen())
                {
                    string st = "SELECT Nome, s.Id, s.Entidade, s.NumDoc, s.Data FROM CLIENTES INNER JOIN (SELECT DISTINCT TOP " + number + "  CABECDOC.Id, CABECDOC.NumDoc, CABECDOC.Entidade, LINHASDOC.Data FROM CABECDOC, LINHASDOC WHERE LINHASDOC.IdCabecDoc = CABECDOC.Id AND LINHASDOC.Artigo = '" + productID + "' ORDER BY LINHASDOC.Data DESC) s on s.Entidade = CLIENTES.Cliente";

                    objListLinhas = PriEngine.Engine.Consulta(st);

                    System.Diagnostics.Debug.WriteLine(objListLinhas.NumLinhas());
                    while (!objListLinhas.NoFim())
                    {
                        listSalesOrder.Add(new Model.SalesOrderHistory
                        {
                            name = objListLinhas.Valor("Nome"),
                            date = objListLinhas.Valor("Data").ToString(),
                            customerID = objListLinhas.Valor("Entidade"),
                            salesID = objListLinhas.Valor("Id"),
                            numDoc = objListLinhas.Valor("NumDoc").ToString()
                        });

                        objListLinhas.Seguinte();
                    }
                    return listSalesOrder;
                }
                return listSalesOrder;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return listSalesOrder;
            }
        }

        #endregion SalesOrder


        #region RoutesCalendar

        public static List<Model.RoutesCalendar> ListRoutes()
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();

            List<Model.RoutesCalendar> lstlindv = new List<Model.RoutesCalendar>();

            return lstlindv;
        }

        #endregion RoutesCalendar


        #region Labels
        public static IEnumerable<Model.Customer> ListCostumerByLabel(string labelId)
        {
            List<Model.Customer> listCustomers = new List<Model.Customer>();
            
            if (PriEngine.isOpen())
            {

                string query = "SELECT * FROM  CLIENTES WHERE CDU_CampoVar1 ='" + labelId + "' OR CDU_CampoVar2 = '" + labelId + "' OR CDU_CampoVar3 = '" + labelId + "'";
                StdBELista objList = PriEngine.Engine.Consulta(query);
                List<string> labelsList;

                while (!objList.NoFim())
                {
                    labelsList = new List<string>();
                    labelsList.Add(objList.Valor("CDU_CampoVar1"));
                    labelsList.Add(objList.Valor("CDU_CampoVar2"));
                    labelsList.Add(objList.Valor("CDU_CampoVar3"));

                    listCustomers.Add(new Model.Customer
                    {
                        id = objList.Valor("Cliente"),
                        name = objList.Valor("Nome"),
                        phoneNumber = objList.Valor("Fac_Tel"),
                        address = objList.Valor("Fac_Mor"),
                        email = objList.Valor("B2BEnderecoMail"),
                        //customerGroups = objList.Valor("CDU_GruposDeClientes"),
                        //gender = objList.Valor("CDU_Sexo"),
                        //dateOfBirth = objList.Valor("CDU_DataNascimento").ToString(),
                        nationality = objList.Valor("Pais"),
                        nif = objList.Valor("NumContrib"),
                        labels = labelsList
                    });
                    objList.Seguinte();

                }

                return listCustomers;
            }
            else
                return null;
        }

        public static Model.ErrorResponse AddLabelToCostumer(string costumerId, string labelId)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBECliente objCli = new GcpBECliente();
            StdBECampos camposUteis;
            StdBECampos novosCampos = new StdBECampos();
            
            bool result = false;
            try
            {
                if (PriEngine.isOpen())
                {
                    if (!PriEngine.Engine.Comercial.Clientes.Existe(costumerId))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "Client Not Found";
                        return erro;
                    }
                    else
                    {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(costumerId);
                        objCli.set_EmModoEdicao(true);

                        camposUteis = objCli.get_CamposUtil();
                        foreach(StdBECampo campo in camposUteis)
                        {
                            string valor;
                            if (campo.Valor is DBNull)
                            {
                                valor = "";
                            }
                            else
                            {
                                valor = campo.Valor;
                            }

                            valor = valor.Trim();
                            if (valor == "" && !result)
                            {
                                campo.Valor = labelId;
                                result = true;
                            }

                            novosCampos.Insere(campo);
                            continue;
                            
                        }

                        if (result)
                        {
                            objCli.set_CamposUtil(novosCampos);
                            PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);
                            erro.Erro = 0;
                            erro.Descricao = "Sucess";
                            
                        }else
                        {
                            erro.Erro = 1;
                            erro.Descricao = "Client already has 3 labels, can't have more";
                        }

                        
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Error accessing Database";
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

        public static Model.ErrorResponse DeleteLabelToCostumer(string costumerId, string label)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Model.ErrorResponse();
            GcpBECliente objCli = new GcpBECliente();
            StdBECampos camposUteis;
            StdBECampos novosCampos = new StdBECampos();

            bool result = false;
            try
            {
                if (PriEngine.isOpen())
                {
                    if (!PriEngine.Engine.Comercial.Clientes.Existe(costumerId))
                    {
                        erro.Erro = 1;
                        erro.Descricao = "Client Not Found";
                        return erro;
                    }
                    else
                    {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(costumerId);
                        objCli.set_EmModoEdicao(true);

                        camposUteis = objCli.get_CamposUtil();
                        foreach (StdBECampo campo in camposUteis)
                        {
                            string valor;
                            if (campo.Valor is DBNull)
                            {
                                valor = "";
                            }
                            else
                            {
                                valor = campo.Valor;
                            }

                            valor = valor.Trim();
                            if (valor == label)
                            {
                                campo.Valor = DBNull.Value;
                                result = true;
                            }

                            novosCampos.Insere(campo);
                            continue;

                        }

                        if (result)
                        {
                            objCli.set_CamposUtil(novosCampos);
                            PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);
                            erro.Erro = 0;
                            erro.Descricao = "Sucess";

                        }
                        else
                        {
                            erro.Erro = 1;
                            erro.Descricao = "Client doesn't have that label";
                        }


                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Error accessing Database";
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

        #endregion Labels


        #region Countries

        public static IEnumerable<Model.Country> GetCountries()
        {
            List<Lib_Primavera.Model.Country> countries = new List<Model.Country>();

            if (PriEngine.isOpen())
            {
                string query = "SELECT Pais, Descricao FROM Paises";
                StdBELista objList = PriEngine.Engine.Consulta(query);
                Lib_Primavera.Model.Country country;
                while (!objList.NoFim())
                {
                    country = new Model.Country();
                    country.country = objList.Valor("Pais");
                    country.descricao = objList.Valor("Descricao");
                    countries.Add(country);

                    objList.Seguinte();
                }

                return countries;
            }
            else
            {
                return null;
            }

        }

        #endregion Countries


        #region Stats

        public static IEnumerable<Model.Stats.IncomeYear> GetIncomeStatBySalesRep(string salesRepId)
        {
            int initYear = 2015;
            List<Model.Stats.IncomeYear> list = new List<Model.Stats.IncomeYear>();
            Model.Stats.IncomeYear year;

            if (PriEngine.isOpen())
            {
                for (int i = initYear; i <= DateTime.Now.Year; i++)
                {
                    year = GetIncomeStatByYear(salesRepId, i);
                    list.Add(year);
                }
                

                return list;
            }
            else
            {
                return null;
            }
        }
        
        public static Model.Stats.IncomeYear GetIncomeStatByYear(string salesRepId, int year)
        {
            Model.Stats.IncomeYear yearStat = new Model.Stats.IncomeYear();
            Model.Stats.IncomeMonth monthStat;
            if (PriEngine.isOpen())
            {
                yearStat.year = year;
                yearStat.sales = new List<Model.Stats.IncomeMonth>();
                yearStat.totalIncome = 0;

                for (int i = 1; i <= 12; i++)
                {
                    monthStat = GetIncomeStatByMonth(salesRepId, year, i);
                    yearStat.totalIncome += monthStat.income;
                    yearStat.sales.Add(monthStat);
                }
               
                

                return yearStat;
            }
            else
            {
                return null;
            }
        }

        public static Model.Stats.IncomeMonth GetIncomeStatByMonth(string salesRepId, int year, int month)
        {
            Model.Stats.IncomeMonth monthStat = new Model.Stats.IncomeMonth();

            if (PriEngine.isOpen())
            {

                string query = "SELECT SUM(TotalMerc) as Total FROM CabecDoc WHERE TipoDoc='ECL' AND Responsavel = '" + salesRepId+"' AND Month(Data)=" + month + " AND Year(Data)="+year;
                StdBELista objList = PriEngine.Engine.Consulta(query);
                if (objList.Valor("Total") is DBNull || "".Equals(objList.Valor("Total")))
                {
                    monthStat.income = 0;
                }
                else
                {
                    monthStat.income = objList.Valor("Total");
                }
                
                monthStat.month = month;
                return monthStat;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<Model.Stats.SalesYear> GetSalesStatBySalesRep(string salesRepId)
        {
            int initYear = 2015;
            List<Model.Stats.SalesYear> list = new List<Model.Stats.SalesYear>();
            Model.Stats.SalesYear year;

            if (PriEngine.isOpen())
            {
                for (int i = initYear; i <= DateTime.Now.Year; i++)
                {
                    year = GetSalesStatByYear(salesRepId, i);
                    list.Add(year);
                }


                return list;
            }
            else
            {
                return null;
            }
        }

        public static Model.Stats.SalesYear GetSalesStatByYear(string salesRepId, int year)
        {
            Model.Stats.SalesYear yearStat = new Model.Stats.SalesYear();
            Model.Stats.SalesMonth monthStat;
            if (PriEngine.isOpen())
            {
                yearStat.year = year;
                yearStat.sales = new List<Model.Stats.SalesMonth>();
                yearStat.salesNumber = 0;

                for (int i = 1; i <= 12; i++)
                {
                    monthStat = GetSalesStatByMonth(salesRepId, year, i);
                    yearStat.salesNumber += monthStat.salesNumber;
                    yearStat.sales.Add(monthStat);
                }



                return yearStat;
            }
            else
            {
                return null;
            }
        }

        public static Model.Stats.SalesMonth GetSalesStatByMonth(string salesRepId, int year, int month)
        {
            Model.Stats.SalesMonth monthStat = new Model.Stats.SalesMonth();

            if (PriEngine.isOpen())
            {

                string query = "SELECT count(*) as Total FROM CabecDoc WHERE TipoDoc='ECL' AND Responsavel = '" + salesRepId+"' AND Month(Data)=" + month + " AND Year(Data)="+year;
                StdBELista objList = PriEngine.Engine.Consulta(query);

                if (objList.Valor("Total") is DBNull || "".Equals(objList.Valor("Total")))
                {
                    monthStat.salesNumber = 0;
                }
                else
                {
                    monthStat.salesNumber = objList.Valor("Total");
                }
                
                monthStat.month = month;
                return monthStat;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<Model.Stats.IncomePerYear> GetIncomePerYearBySalesRep(string salesRepId)
        {
            int initYear = 2015;
            List<Model.Stats.IncomePerYear> list = new List<Model.Stats.IncomePerYear>();
            Model.Stats.IncomePerYear year;

            if (PriEngine.isOpen())
            {
                for (int i = initYear; i <= DateTime.Now.Year; i++)
                {
                    year = GetIncomePerYear(salesRepId, i);
                    list.Add(year);
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public static Model.Stats.IncomePerYear GetIncomePerYear(string salesRepId, int year)
        {
            Model.Stats.IncomePerYear yearStat = new Model.Stats.IncomePerYear();
            Model.Stats.IncomePerMonth monthStat;
            if (PriEngine.isOpen())
            {
                yearStat.year = year;
                yearStat.monthRates = new List<Model.Stats.IncomePerMonth>();

                for (int i = 1; i <= 12; i++)
                {
                    monthStat = GetIncomePerMonth(salesRepId, year, i);
                    yearStat.incomePerYear = GetIncomeStatByYear(salesRepId,year).totalIncome/GetSalesStatByYear(salesRepId,year).salesNumber;
                    yearStat.monthRates.Add(monthStat);
                }



                return yearStat;
            }
            else
            {
                return null;
            }
        }

        public static Model.Stats.IncomePerMonth GetIncomePerMonth(string salesRepId, int year, int month)
        {
            Model.Stats.IncomePerMonth monthStat = new Model.Stats.IncomePerMonth();

            if (PriEngine.isOpen())
            {

                double rate = GetIncomeStatByMonth(salesRepId, year, month).income / GetSalesStatByMonth(salesRepId, year, month).salesNumber;
                monthStat.incomePerMonth = rate;
                monthStat.month = month;
                return monthStat;
            }
            else
            {
                return null;
            }
        }

        public static int GetTotalSalesNumByCategories(string salesRepId)
        {
            

            if (PriEngine.isOpen())
            {
                string query = "SELECT Count(Familias.Familia) as Contagem "+
                                "FROM PRIDEMOSINF.dbo.Artigo, PRIDEMOSINF.dbo.Familias, PRIDEMOSINF.dbo.CabecDoc, PRIDEMOSINF.dbo.LinhasDoc "+ 
                                "WHERE		CabecDoc.TipoDoc = 'ECL' "+
		                                "AND	CabecDoc.Id = LinhasDoc.IdCabecDoc "+
		                                "AND LinhasDoc.Artigo = Artigo.Artigo "+
		                                "AND Artigo.Familia = Familias.Familia "+
		                                "AND CabecDoc.Responsavel = '1' ";
                StdBELista objList = PriEngine.Engine.Consulta(query);
                return objList.Valor("Contagem");
                
            }
            else
            {
                return 0;
            }
        }

        public static IEnumerable<Model.Stats.TopCategory> GetSalesTopCategories(string salesRepId)
        {
            int total = GetTotalSalesNumByCategories(salesRepId);
            List<Model.Stats.TopCategory> list = new List<Model.Stats.TopCategory>();
            Model.Stats.TopCategory topCategory;
            Model.Category category;

            if (PriEngine.isOpen())
            {
                string query = "SELECT Familias.Familia as Familia, Familias.Descricao as Descricao, Count(Familias.Familia) as Contagem " +
                                "FROM PRIDEMOSINF.dbo.Artigo, PRIDEMOSINF.dbo.Familias, PRIDEMOSINF.dbo.CabecDoc, PRIDEMOSINF.dbo.LinhasDoc " +
                                "WHERE		CabecDoc.TipoDoc = 'ECL' " +
                                        "AND	CabecDoc.Id = LinhasDoc.IdCabecDoc " +
                                        "AND LinhasDoc.Artigo = Artigo.Artigo " +
                                        "AND Artigo.Familia = Familias.Familia " +
                                        "AND CabecDoc.Responsavel = '1' " +
                                "GROUP BY Familias.Familia, Familias.Descricao " +
                                "ORDER BY Contagem DESC";
                StdBELista objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    topCategory = new Model.Stats.TopCategory();
                    topCategory.numSales = (int) objList.Valor("Contagem");
                    topCategory.percent = topCategory.numSales / (double) total;
                    category = new Model.Category();
                    category.family = objList.Valor("Familia");
                    category.description = objList.Valor("Descricao");
                    topCategory.category = category;
                    list.Add(topCategory);
                    objList.Seguinte();
                }

                return list;
            }
            else
            {
                return null;
            }

        }



        #endregion Stats

    }
}