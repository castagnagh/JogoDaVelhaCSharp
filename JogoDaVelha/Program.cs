using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace JogoDaVelha
{
    internal class Program
    {
        public static char[,] tabuleiro = new char[3, 3];
        static void Main(string[] args)
        {
            char continuar = 'S';
            while (continuar == 'S')
            {
                exibirInstrucoes();
                //escolha da opção mostrada nas instrucoes
                string escolha = Console.ReadLine();
                Console.Clear();
                if (escolha == "1")
                {
                    int jogadas = 0;
                    bool jogoDecidido = false;
                    //opção = 1 (logica para vs pessoa)
                    inicializaTabuleiro();
                    exibirTabuleiro();
                    while (!jogoDecidido)
                    {
                        escolhaJogador();
                        exibirTabuleiro();
                        if (verificaVencedor('X'))
                        {
                            Console.WriteLine("---------PARABÉNS---------");
                            Console.WriteLine("O jogador X ganhou o jogo");
                            jogoDecidido = true;
                        }
                        else if (verificaVencedor('O'))
                        {
                            Console.WriteLine("---------PARABÉNS---------");
                            Console.WriteLine("O jogador O ganhou o jogo");
                            jogoDecidido = true;
                        }
                        else if (jogadas >= 8)
                        {
                            Console.WriteLine("-----------EMPATE-----------");
                            Console.WriteLine("Não foi dessa vez, deu VELHA");
                            jogoDecidido = true;
                        }
                        jogadas++;
                    }
                }
                else if (escolha == "2")
                {
                    int jogadas = 0;
                    bool jogoDecidido = false;

                    inicializaTabuleiro();

                    while (!jogoDecidido)
                    {
                        // Verificar se é a vez do computador (X) ou do jogador (O)
                        char jogadorAtual;

                        if (jogadas % 2 == 0)
                        {
                            jogadorAtual = 'X';
                            Console.WriteLine("JOGADA DO COMPUTADOR: ");
                        }
                        else
                        {
                            jogadorAtual = 'O';
                            Console.WriteLine("SUA VEZ");

                        }

                        if (jogadorAtual == 'X')
                        {
                            //aqui testo se tem 2 elementos na mesma linha, coluna ou vertical (teste para O e para X)...
                            //se dois elementos X na mesma linha, coluna ou vertical então o X é marcado.. e assim para caso tiver dois elementos O
                            if (verificaLinha())
                            {
                                preencheTerceiraPosicao();
                                exibirTabuleiro();
                            }
                            else //se não tem, então ele preenche os cantos (jogada inicial)
                            {
                                preencheCantos();
                                exibirTabuleiro();
                            }
                        }
                        else
                        {
                            jogadorJogaVsPC();
                        }

                        if (verificaVencedor('X'))
                        {
                            Console.WriteLine("---------DERROTA---------");
                            Console.WriteLine("O Computador ganhou o jogo");
                            jogoDecidido = true;
                        }
                        else if (verificaVencedor('O'))
                        {
                            Console.WriteLine("---------PARABÉNS---------");
                            Console.WriteLine("Você ganhou o jogo");
                            jogoDecidido = true;
                        }
                        else if (jogadas >= 8)
                        {
                            Console.WriteLine("-----------EMPATE-----------");
                            Console.WriteLine("Não foi dessa vez, deu VELHA");
                            jogoDecidido = true;
                        }

                        //incrementa numero de jogadas, para verificar quem joga e se há empate
                        jogadas++;
                    }
                }
                else
                {
                    Console.WriteLine("Fim do jogo!");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------");
                Console.WriteLine("Deseja jogar outra vez?");
                Console.WriteLine("S - para SIM");
                Console.WriteLine("Qualquer tecla para encerrar!");
                continuar = char.Parse(Console.ReadLine());
                Console.Clear();
                numeroJogadas = 0;

            }
        }

        static void exibirInstrucoes()
        {
            Console.WriteLine("                                       JOGO DA VELHA");
            Console.WriteLine();
            Console.WriteLine("===========================================REGRAS===========================================");
            Console.WriteLine();
            Console.WriteLine("* - Para jogar você deve escolher a linha e a coluna, apertar ENTER após cada escolha;");
            Console.WriteLine("* - A Linha (L) e a Coluna(C) começando em 1 e terminando em 3, representadas no desenho abaixo.");
            Console.WriteLine("* - O primeiro a jogar sempre é o X");
            Console.WriteLine("* - Caso escolha jogar contra o Computador ele SEMPRE será o X");
            Console.WriteLine();
            Console.WriteLine("                       C:1   C:2   C:3");
            Console.WriteLine();
            Console.WriteLine("                 L:1    ?     ?     ?");
            Console.WriteLine("                 L:2    ?     ?     ?");
            Console.WriteLine("                 L:3    ?     ?     ?");
            Console.WriteLine();
            Console.WriteLine("========================ESCOLHA========================");
            Console.WriteLine();
            Console.WriteLine("Digite 1 para Player vs Player");
            Console.WriteLine("Digite 2 para Computador vs Player");
            Console.WriteLine();
            Console.WriteLine("Digite QUALQUER TECLA para sair");
        }

        //mostra a matriz inicial (inicializa tabuleiro), preenche todas as posições com '?'
        static void inicializaTabuleiro()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    tabuleiro[i, j] = '?';
                }
            }
        }
        //variavel criada para verificar se a jogada é PAR ou IMPAR
        static int numeroJogadas = 0;

        //metodo criado para jogada do Jogador vs Jogador
        static void escolhaJogador()
        {
            if (numeroJogadas % 2 == 0)
            {
                Console.WriteLine("VEZ JOGADOR 'X': ");
            }
            else
            {
                Console.WriteLine("VEZ JOGADOR '0': ");

            }
            Console.Write("Escolha uma linha (L): ");
            int linha = int.Parse(Console.ReadLine());
            Console.Write("Escolha uma coluna (C): ");
            int coluna = int.Parse(Console.ReadLine());

            //verifica se a posição esta ocupada e qual jogador esta jogando se X ou O
            //-> PAR para o Jogador X e IMPAR para o Jogador 0
            if (tabuleiro[linha - 1, coluna - 1] == '?')
            {
                if (numeroJogadas % 2 == 0)
                {
                    tabuleiro[linha - 1, coluna - 1] = 'X';
                }
                else
                {
                    tabuleiro[linha - 1, coluna - 1] = 'O';
                }
                Console.Clear();
                numeroJogadas++;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Posição já ocupada, escolha uma posição novamente");
                exibirTabuleiro();
                escolhaJogador();
                return;
            }
        }

        //jogadorJogaVsPC -> Metodo implementado para Player vs PC
        static void jogadorJogaVsPC()
        {
            Console.Write("Informe linha (L): ");
            int linha = int.Parse(Console.ReadLine());
            Console.Write("Informe coluna (C): ");
            int coluna = int.Parse(Console.ReadLine());
            Console.Clear();
            if (tabuleiro[linha - 1, coluna - 1] == '?')
            {
                tabuleiro[linha - 1, coluna - 1] = 'O';
            }
            else
            {
                exibirTabuleiro();
                Console.WriteLine("Posição já ocupada, escolha uma posição novamente");
                jogadorJogaVsPC();
            }
        }

        //PASSO 1 - PREENCHER OS CANTOS
        //(tabuleiro[0,0], ...02, ...20, ... 22
        static void preencheCantos()
        {
            Random random = new Random();

            bool cantoPreenchido = false;

            //enquanto o canto não for TRUE ele continua no laço até preencher o canto, 
            //quando preencher o laço termina
            while (!cantoPreenchido)
            {
                //aqui ele multiplica por 2 para ter o numero 0 e 2 sorteados, pois
                // ele pode sortear apenas o 0 ou 1, multiplicado por 2 fica -> 0 *2 = 0 && 1*2 =2
                int linhaSorte = random.Next(2) * 2;
                int colunaSorte = random.Next(2) * 2;
                if (tabuleiro[linhaSorte, colunaSorte] == '?')
                {
                    tabuleiro[linhaSorte, colunaSorte] = 'X';
                    cantoPreenchido = true;
                }
            }
        }

        //PASSO 3 - Verifica dois X na mesma linha, coluna e vertical para preenchimento do 3º X -> metodo criado apenas para validar, pois precisava fazer o teste na condição do player vs computador
        static bool verificaLinha()
        {//verifica primeira linha
            bool status = false;
            if ((tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[0, 2] && tabuleiro[0, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[0, 2] && tabuleiro[0, 0] == '?'))
            {
                status = true;
            }
            //verifica a segunda linha
            else if ((tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 0] == tabuleiro[1, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[1, 2] && tabuleiro[1, 0] == '?'))
            {
                status = true;
            }
            //verifica a terceira linha
            else if ((tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[2, 0] == tabuleiro[2, 2] && tabuleiro[2, 1] == '?') || (tabuleiro[2, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {
                status = true;
            }
            //verifica primeira coluna
            else if ((tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 0] && tabuleiro[1, 0] == '?') || (tabuleiro[1, 0] == tabuleiro[2, 0] && tabuleiro[0, 0] == '?'))
            {
                status = true;
            }
            //verifica segunda coluna
            else if ((tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[2, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[2, 1] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 1] && tabuleiro[0, 1] == '?'))
            {
                status = true;
            }
            //verifica terceira coluna
            else if ((tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 2] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 2] == tabuleiro[2, 2] && tabuleiro[0, 2] == '?'))
            {
                status = true;
            }
            //verifica diagonal principal
            else if ((tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {
                status = true;
            }
            //verifica diagonal secundaria
            else if ((tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 0] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 0] && tabuleiro[0, 2] == '?'))
            {
                status = true;
            }
            return status;
        }

        //metedo criadado para preencher a terceira posição caso o verificaLinha() fosse TRUE
        static void preencheTerceiraPosicao()
        {
            if ((tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[0, 2] && tabuleiro[0, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[0, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[0, j] == '?')
                    {
                        tabuleiro[0, j] = 'X';
                        break;
                    }
                }
            }
            //verifica a segunda linha
            else if ((tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 0] == tabuleiro[1, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[1, 2] && tabuleiro[1, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[1, j] == '?')
                    {
                        tabuleiro[1, j] = 'X';
                        break;
                    }
                }
            }
            //verifica a terceira linha
            else if ((tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[2, 0] == tabuleiro[2, 2] && tabuleiro[2, 1] == '?') || (tabuleiro[2, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[2, j] == '?')
                    {
                        tabuleiro[2, j] = 'X';
                        break;
                    }
                }
            }
            //verifica primeira coluna
            else if ((tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 0] && tabuleiro[1, 0] == '?') || (tabuleiro[1, 0] == tabuleiro[2, 0] && tabuleiro[0, 0] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 0] == '?')
                    {
                        tabuleiro[i, 0] = 'X';
                        break;
                    }
                }
            }
            //verifica segunda coluna
            else if ((tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[2, 1] == '?') || (tabuleiro[0, 1] == tabuleiro[2, 1] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 1] && tabuleiro[0, 1] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 1] == '?')
                    {
                        tabuleiro[i, 1] = 'X';
                        break;
                    }
                }
            }
            //verifica terceira coluna
            else if ((tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 2] && tabuleiro[1, 2] == '?') || (tabuleiro[1, 2] == tabuleiro[2, 2] && tabuleiro[0, 2] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, 2] == '?')
                    {
                        tabuleiro[i, 2] = 'X';
                        break;
                    }
                }
            }
            //verifica diagonal principal
            else if ((tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[2, 2] == '?') || (tabuleiro[0, 0] == tabuleiro[2, 2] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 2] && tabuleiro[0, 0] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, i] == '?')
                    {
                        tabuleiro[i, i] = 'X';
                        break;
                    }
                }
            }
            //verifica diagonal secundaria
            else if ((tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[2, 0] == '?') || (tabuleiro[0, 2] == tabuleiro[2, 0] && tabuleiro[1, 1] == '?') || (tabuleiro[1, 1] == tabuleiro[2, 0] && tabuleiro[0, 2] == '?'))
            {

                for (int i = 0; i < tabuleiro.GetLength(0); i++)
                {
                    if (tabuleiro[i, i] == '?')
                    {
                        tabuleiro[i, i] = 'X';
                        break;
                    }
                }
            }

        }

        //metodo criado para exibir o tabuleiro depois do jogador escolher a posição
        static void exibirTabuleiro()
        {
            Console.WriteLine("         C:1 C:2 C:3");
            Console.WriteLine();
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                Console.Write("    ");
                Console.Write("L:" + (i+1));
                Console.Write("   ");
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("         ----------");
            }
            Console.WriteLine();
        }

        static bool verificaVencedor(char simbolo)
        {
            bool status = false;
            //testando as horizontais
            if (tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 1] == tabuleiro[0, 2] && tabuleiro[0, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[1, 2] && tabuleiro[1, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 1] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            //testando as verticais
            else if (tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[1, 0] == tabuleiro[2, 0] && tabuleiro[2, 0] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 1] && tabuleiro[2, 1] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[1, 2] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            //testando verticais
            else if (tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2] && tabuleiro[2, 2] == simbolo)
            {
                status = true;
            }
            else if (tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 0] && tabuleiro[2, 0] == simbolo)
            {
                status = true;
            }
            return status;
        }
    }
}
