using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjetoAcessos
{
    class Program
    {
        static void Main(string[] args)
        {

            int opcao = 0;
            int idAmb, idUsu;
            string nomeAmb, nomeUsu;
            Cadastro c = new Cadastro();
            Usuario u = new Usuario();
            Ambiente a = new Ambiente();
            string padding = "";
            for (int cont = 1; cont <= 4; cont++)
            {
                Thread.Sleep(1000);
                Console.Clear();
                if (cont != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\n\n\n\n\n\n" + string.Format("{0,50} {1,70}", "Carregando Banco de Dados", "Aguarde..."));
                    Console.ResetColor();
                }
            }

            c.Download();

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================ Menu ================");
                Console.WriteLine("0. Sair\n1. Cadastrar Ambiente\n2. Consultar ambiente\n3. Excluir ambiente\n4. Cadastrar usuário\n5. Consultar usuário\n6. Excluir usuário\n7. Conceder Permissão de acesso ao usuário\n8. Revogar permissão de acesso ao usuário\n9. Registrar acesso\n10. Consultar logs de acesso\n");
                opcao = int.Parse(Console.ReadLine());
                Console.ResetColor();
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("==== Adicionar Ambiente ====");
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o nome do ambiente:");
                        nomeAmb = Console.ReadLine();
                        c.AdicionarAmbiente(new Ambiente(idAmb, nomeAmb));
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("==== Pesquisar Ambiente ====");
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());
                        c.PesquisarAmbiente(new Ambiente(idAmb, ""));
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("==== Excluir Ambiente ====");
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());
                        c.RemoverAmbiente(new Ambiente(idAmb, ""));
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("==== Adicionar Usuário ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o nome do usuário:");
                        nomeUsu = Console.ReadLine();
                        c.AdicionarUsuario(new Usuario(idUsu, nomeUsu));
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("==== Pesquisar Usuário ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Usuario temp3 = c.PesquisarUsuario(new Usuario(idUsu, ""));
                        temp3.listarAmbientes();
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("==== Excluir Usuário ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Usuario temp7 = c.PesquisarUsuario(new Usuario(idUsu, ""));
                        c.RemoverUsuario(temp7);
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("==== Conceder Acesso ao Usuário ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Usuario temp = c.PesquisarUsuario(new Usuario(idUsu, ""));
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());
                        Ambiente newAmb = c.PesquisarAmbiente(new Ambiente(idAmb, ""));
                        temp.ConcederPermissao(newAmb);
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("==== Revogar Acesso ao Usuário ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Usuario temp2 = c.PesquisarUsuario(new Usuario(idUsu, ""));
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());
                        Ambiente newAmb2 = c.PesquisarAmbiente(new Ambiente(idAmb, ""));
                        Conexao.deletePermissoes(temp2,newAmb2);
                        temp2.RevogarPermissao(newAmb2);
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("==== Registrar Acesso ====");
                        Console.WriteLine("Digite o ID do usuário:");
                        idUsu = int.Parse(Console.ReadLine());
                        Usuario temp4 = c.PesquisarUsuario(new Usuario(idUsu, ""));
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());

                        foreach (Ambiente amb in temp4.Ambientes)
                        {
                            if (amb.Id.Equals(idAmb))
                            {
                                amb.RegistrarLog(new Log(DateTime.Now, temp4, true));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nACESSO PERMITIDO.");
                                Console.ResetColor();
                            }
                            else
                            {
                                amb.RegistrarLog(new Log(DateTime.Now, temp4, false));
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nACESSO NEGADO.");
                                Console.ResetColor();
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 10:
                        Console.Clear();
                        Console.WriteLine("==== Consultar Logs ====");
                        Console.WriteLine("Digite o ID do ambiente:");
                        idAmb = int.Parse(Console.ReadLine());

                        foreach (Usuario user in c.Usuarios)
                        {
                            foreach (Ambiente amb in user.Ambientes)
                            {
                                if (amb.Id.Equals(idAmb))
                                {
                                    foreach (Log logs in amb.Logs)
                                    {
                                        Console.WriteLine(logs.toString());
                                    }
                                }
                            }
                        }
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida!!");
                        break;
                }

            } while (opcao > 0);
            c.Upload(c);
        }
    }
}
