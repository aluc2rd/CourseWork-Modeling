using System; // Пространство имен System, содержащее основные классы и структуры для работы с памятью, вводом-выводом, обработкой исключений и т.д.

namespace CourseWork // Пространство имен CourseWork
{
    public class MicroProgram // Класс MicroProgram, реализующий микропрограмму для выполнения операций
    {
        // Регистр A для хранения числа A
        private ushort _a;

        // Регистр B для хранения числа B
        private ushort B { get; set; }

        // Регистр C для хранения числа C
        private uint C { get; set; }

        // Дополнительный регистр D
        private byte _d;

        // Текущая позиция в микропрограмме
        private byte _currentPosition;

        // Массив действий, соответствующих ключевым словам Y
        private readonly Action[] _yArray;

        // Массив условий, соответствующих предикатам X
        private readonly bool[] _x;

        // Ссылка на главный форму
        private readonly MainForm _mainForm;

        // Флаг, указывающий, были ли внесены данные в автомат
        private bool InstallData { get; set; } = true;

        // Счетчик для управления циклом
        private byte Count { get; set; }

        // Конструктор класса MicroProgram
        public MicroProgram(MainForm main)
        {
            // Инициализация массива условий X
            _x = new bool[10];
            _x[0] = true; // X0 всегда истинно

            // Сохранение ссылки на главный форму
            _mainForm = main;

            // Инициализация массива действий Y
            _yArray = new Action[]
            {
                // y1: C = 0
                () => { C = 0; },

                // y2: D = B >> 15
                () => { _d = (byte)(B >> 15); },

                // y3: B = (B << 1) >> 1
                () => { B = (ushort)((ushort)(B << 1) >> 1); },

                // y4: Count = 0
                () => { Count = 0; },

                // y5: C += (A << 17) >> 3
                () => { C += (uint)(_a << 17) >> 3; },

                // y6: C += (A << 17) >> 2
                () => { C += (uint)(_a << 17) >> 2; },

                // y7: C += ~(A << 17) + 1
                () =>
                {
                    uint buff = (uint)(~(_a << 17 >> 17) + 0x01) | 0xC0000000;
                    C = C + (buff << 14) + 0x1;
                },

                // y8: B = B + 1
                () => { B = (ushort)(B + 0x01); },

                // y9: Count = 7, если Count = 0, иначе Count--
                () =>
                {
                    Count = (byte)(Count == 0
                            ? 7
                            : Count - 1
                        );
                },

                // y10: B = (B << 2) >> 4
                () => { B = (ushort)((B << 2) >> 4); },

                // y11: C = (C >> 2) + ((C >> 31) << 30)
                () =>
                {
                    var buff = C >> 31;
                    buff = ((buff << 1) | buff) << 30;
                    C = (C >> 2) + buff;
                },

                // y12: C += 0x10000
                () => { C += 0x10000; },

                // y13: C |= 0x80000000
                () => { C |= 0x80000000; }
            };
        }

        // Метод для ввода данных в автомат
        public void InputData(ushort a, ushort b)
        {
            if (InstallData) // Если данные еще не были внесены
            {
                _a = a; // Установка числа A
                B = b; // Установка числа B
                InstallData = false; // Установка флага внесения данных
            }
        }

        // Метод для запуска микропрограммы
        public void AllRun()
        {
            if (_mainForm.radioButton_x0_0.Checked) // Если выбран режим X0-0, выходим из метода
                return;

            while (Tact()) // Выполняем такты до тех пор, пока Tact() возвращает true
            {
            }
        }

        // Метод для выполнения одного такта
        public bool Tact()
        {
            switch (_currentPosition) // В зависимости от текущей позиции в микропрограмме
            {
                case 0: // Первый такт
                    if (CalculateX(0)) // Если X0 истинно
                    {
                        if (CalculateX(1) || CalculateX(2)) // Если X1 или X2 истинно
                        {
                            _yArray[0](); // Выполняем действие y1
                            _currentPosition = 0; // Оставляем текущую позицию без изменений
                        }
                        else
                        {
                            _yArray[0](); // Выполняем действие y1
                            _yArray[1](); // Выполняем действие y2
                            _yArray[2](); // Выполняем действие y3
                            _yArray[3](); // Выполняем действие y4
                            _currentPosition = 1; // Переходим к следующей позиции
                        }
                    }

                    break;
                case 1: // Второй такт
                    if (CalculateX(3)) // Если X3 истинно
                    {
                        if (CalculateX(6)) // Если X6 истинно
                        {
                            _yArray[8](); // Выполняем действие y9
                        }
                        else
                        {
                            _yArray[8](); // Выполняем действие y9
                            _yArray[9](); // Выполняем действие y10
                            _yArray[10](); // Выполняем действие y11
                        }

                        _currentPosition = 3; // Переходим к позиции 3
                        break;
                    }

                    if (CalculateX(4)) // Если X4 истинно
                        _yArray[4](); // Выполняем действие y5
                    else if (CalculateX(5)) // Если X5 истинно
                        _yArray[5](); // Выполняем действие y6
                    else
                    {
                        _yArray[6](); // Выполняем действие y7
                        _yArray[7](); // Выполняем действие y8
                    }

                    _currentPosition = 2; // Переходим к позиции 2
                    break;
                case 2: // Третий такт
                    if (CalculateX(6)) // Если X6 истинно
                        _yArray[8](); // Выполняем действие y9
                    else
                    {
                        _yArray[8](); // Выполняем действие y9
                        _yArray[9](); // Выполняем действие y10
                        _yArray[10](); // Выполняем действие y11
                    }

                    _currentPosition = 3; // Переходим к позиции 3
                    break;
                case 3: // Четвертый такт
                    if (CalculateX(7)) // Если X7 истинно
                    {
                        if (CalculateX(8)) // Если X8 истинно
                        {
                            _yArray[11](); // Выполняем действие y12
                            _currentPosition = 4; // Переходим к позиции 4
                        }
                        else
                        {
                            if (CalculateX(9)) // Если X9 истинно
                                _yArray[12](); // Выполняем действие y13
                            _currentPosition = 0; // Переходим к позиции 0
                        }
                    }
                    else
                    {
                        if (CalculateX(3)) // Если X3 истинно
                        {
                            if (CalculateX(6)) // Если X6 истинно
                            {
                                _yArray[8](); // Выполняем действие y9
                            }
                            else
                            {
                                _yArray[8](); // Выполняем действие y9
                                _yArray[9](); // Выполняем действие y10
                                _yArray[10](); // Выполняем действие y11
                            }

                            _currentPosition = 3; // Переходим к позиции 3
                            break;
                        }

                        if (CalculateX(4)) // Если X4 истинно
                            _yArray[4](); // Выполняем действие y5
                        else if (CalculateX(5)) // Если X5 истинно
                            _yArray[5](); // Выполняем действие y6
                        else
                        {
                            _yArray[6](); // Выполняем действие y7
                            _yArray[7](); // Выполняем действие y8
                        }

                        _currentPosition = 2; // Переходим к позиции 2
                    }

                    break;
                case 4: // Пятый такт
                    if (CalculateX(9)) // Если X9 истинно
                        _yArray[12](); // Выполняем действие y13

                    _currentPosition = 0; // Переходим к позиции 0
                    break;
            }

            // Обновление информации в регистрах и состоянии памяти
            _mainForm.UpdateInfoRegister(B, Count, C);
            _mainForm.UpdateStateMemory(_currentPosition);

            return _currentPosition != 0; // Возвращаем true, если текущая позиция не равна 0
        }

        // Метод для сброса данных в автомате
        public void Reset()
        {
            _a = 0; // Сброс числа A
            B = 0; // Сброс числа B
            InstallData = true; // Установка флага внесения данных
            _currentPosition = 0; // Сброс текущей позиции
        }

        // Метод для расчета условий X
        private bool CalculateX(int currentX)
        {
            _x[1] = (_a & 0x7fff) == 0; // X1: A & 0x7fff == 0
            _x[2] = (B & 0x7fff) == 0; // X2: B & 0x7fff == 0
            _x[3] = (B & 0x0003) == 0; // X3: B & 0x0003 == 0
            _x[4] = (B & 0x0003) == 1; // X4: B & 0x0003 == 1
            _x[5] = (B & 0x0003) == 2; // X5: B & 0x0003 == 2
            _x[6] = Count == 1; // X6: Count == 1
            _x[7] = Count == 0; // X7: Count == 0
            _x[8] = C << 16 >> 31 != 0; // X8: C << 16 >> 31 != 0
            _x[9] = ((_a >> 15) ^ _d) == 1; // X9: (_a >> 15) ^ _d == 1

            return _x[currentX]; // Возвращаем значение условия X с заданным индексом
        }
    }
}