using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
    
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSerie();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                    case "c":
                        // erro so executar com Git Bash
                        Console.Clear();
                        break;
                    default:             
                        throw new ArgumentOutOfRangeException("+ ---- Digite uma opção válida ---- +"); 
                }
                
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("+ ====================== THE END ======================== +");
            Console.WriteLine("+ ---- Obrigado por contribuir com SÉRIES INCRÍVEIS! ---- +");
            Console.WriteLine("+ ======================================================= +");

        }

        private static void ListarSerie()
        {
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+ Não há séries disponíveis. Escolha a opção (2) para um novo cadastro +");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                return;
            } else {
                Console.WriteLine("+ ===================================== +");
                Console.WriteLine("+ --------  Séries disponíveis -------- +");
                Console.WriteLine("+ ===================================== +");
                Console.WriteLine();

                foreach (var serie in lista)
                {
                    var excluido = serie.retornaExcluido();

                    Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(),(excluido ? " **Indísponível**" : ""));
                }
            
            }          
        }
        private static void InserirSerie()
        {
            Console.WriteLine("+ ===================================== +");
            Console.WriteLine("+ --------  inserir nova série -------- +");
            Console.WriteLine("+ ===================================== +");
            Console.WriteLine();
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}.{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine();
            Console.Write("Digite o N° do gênero entre as opções acima: ");
            Console.WriteLine();
            int entradaGenero = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            repositorio.Insere(novaSerie);
        }
        private static void AtualizarSerie()
        {
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                Console.WriteLine("+ Não há registros no catálogo +");
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                return;
            } else {
                Console.WriteLine("+ ================================== +");
                Console.WriteLine("+ --------  Editar registro -------- +");
                Console.WriteLine("+ ================================== +");
                Console.WriteLine();
                Console.WriteLine("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}.{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o título da série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de início da série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite a descrição da série: ");
                string entradaDescricao = Console.ReadLine();

                Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);
                
                repositorio.Atualiza(indiceSerie, atualizaSerie);
            }
        }
        private static void ExcluirSerie()
        {
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                Console.WriteLine("+ Não há registros para excluir +");
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                return;
            } else {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("+ =================================== +");
                Console.WriteLine("+ --------  Excluir registro -------- +");
                Console.WriteLine("+ =================================== +");
                Console.WriteLine();

                Console.Write("Deseja realmente excluir essa série? Digite: [S] SIM ou [N] NÃO");
                Console.WriteLine();
                string opcaoUsuario = Console.ReadLine().ToUpper();
             
                if(opcaoUsuario.ToUpper() == "S"){
                    repositorio.Exclui(indiceSerie);
                    Console.WriteLine();
                    Console.WriteLine("Registro excluído do catálogo");
                    return;
                }
            }
        }
        private static void VisualizarSerie()
        {
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                Console.WriteLine("+ Não há registros no catálogo +");
                Console.WriteLine("+++++++++++++++++++++++++++++++++");
                return;
            } else {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine("+ ==================================== +");
                Console.WriteLine("+ --------  Detalhes da Série -------- +");
                Console.WriteLine("+ ==================================== +");
                var serie = repositorio.RetornaPorId(indiceSerie);
                Console.WriteLine(serie);
                Console.WriteLine();

                Console.WriteLine("+ ================ End =============== +");
            }
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("+ =================================== +");
            Console.WriteLine("+ --------  SÉRIES INCRÍVEIS -------- +");
            Console.WriteLine("+ =================================== +");
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada: ");
            Console.WriteLine();
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            Console.WriteLine("+ .................................... +");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

    }
}
