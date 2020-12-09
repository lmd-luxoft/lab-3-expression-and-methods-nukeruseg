using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
        static char win = '-';
        static string[] Players = new string[2];
        static char[] cells = new char[] { '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");
        }
        static void make_move(int player)
        {
            string raw_cell;
            int cell;
            Console.Write(Players[player]);
            do
            {
                Console.Write(",введите номер ячейки,сделайте свой ход:");

                raw_cell = Console.ReadLine();
            }
            while (!Int32.TryParse(raw_cell, out cell));
            while (cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X')
            {
                do
                {
                    Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                    raw_cell = Console.ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }
            cells[cell - 1] = player == 1 ? 'X' : 'O';
        }
        static char check()
        {
            for (int i = 0; i < 3; i++) {
                if (IsHorizontalWin(i) || IsVerticalWin(i) || IsDiagonalWin(i))
                    return cells[i];
            }
            return '-';
        }

        static bool IsHorizontalWin(int idx) {
            return cells[idx * 3] == cells[idx * 3 + 1] && cells[idx * 3 + 1] == cells[idx * 3 + 2];
        }

        static bool IsVerticalWin(int idx)
        {
            return cells[idx] == cells[idx + 3] && cells[idx + 3] == cells[idx + 6];
        }

        static bool IsDiagonalWin(int idx)
        {
            return (cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]);
        }

        static void result()
        {
            var winIdx = win == 'X' ? 0 : 1;
            var loseIdx = win == 'X' ? 1 : 0;
            var winner = Players[winIdx];
            var loser = Players[loseIdx];
            Console.WriteLine($"{winner} вы  выиграли поздравляем {loser} а вы проиграли...");
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                Players[0] = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                Players[1] = Console.ReadLine();
                Console.WriteLine();
            } while (Players[0] == Players[1]);

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                var player = move % 2;
                make_move(player);
                show_cells();
                if (move >= 5)
                {
                    win = check();
                    if (win != '-')
                        break;
                }

            }

            result();
        }
    }
}
