using System; // Пространство имен System, содержащее основные классы и структуры для работы с памятью, вводом-выводом, обработкой исключений и т.д.

namespace CourseWork // Пространство имен CourseWork
{
    /// <summary>
    /// Управляющий автомат.
    /// </summary>
    public class ControlMachine // Класс ControlMachine, реализующий управляющий автомат
    {
        public OperationalMachine _operationalMachine; // Операционный автомат

        /// Метки состояний.
        private bool[] _a;

        /// Термы.
        private bool[] _t;

        /// Выходные данные из D-триггеров.
        private bool[] _d;

        private readonly MainForm _mainForm; // Ссылка на главный форму

        private ushort A { get; set; } // Регистр A для хранения числа A
        private ushort B { get; set; } // Регистр B для хранения числа B

        /// Отвечает за то, были ли внесены данные в автомат
        private bool InstallData { get; set; } = true; // Флаг установки данных

        public ControlMachine(MainForm mainForm) 
        {
            _mainForm = mainForm; // Сохранение ссылки на главный форму

            _t = new bool[21]; // Инициализация массива терм
            _a = new bool[5]; // Инициализация массива меток состояний
            _a[0] = true; // Установка начальной метки состояния
            _d = new bool[3]; // Инициализация массива выходных данных из D-триггеров
        }

        public ControlMachine(ushort a, ushort b)
        {
            _operationalMachine = new OperationalMachine(a, b); // Инициализация операционного автомата
            _t = new bool[21]; // Инициализация массива терм
            _a = new bool[5]; // Инициализация массива меток состояний
            _a[0] = true; // Установка начальной метки состояния
            _d = new bool[3]; // Инициализация массива выходных данных из D-триггеров
        }

        /// Метод для ввода данных в автомат
        public void InputData(ushort a, ushort b)
        {
            if (InstallData) // Если данные еще не были внесены
            {
                A = a; // Установка числа A
                B = b; // Установка числа B
                _operationalMachine = new OperationalMachine(A, B); // Инициализация операционного автомата
                InstallData = false; // Установка флага внесения данных
            }
        }

        /// Отвечает за выход из цикла
        private bool _end;

        /// Метод для запуска управляющего автомата
        public void AllRun()
        {
            if (_mainForm.radioButton_x0_0.Checked) // Если выбран режим X0-0, выходим из метода
                return;

            while (!_end) // Выполняем такты до тех пор, пока _end не станет true
            {
                Tact(); // Выполняем один такт
            }
        }

        /// Метод для выполнения одного такта
        public void Tact()
        {
            _mainForm.UpdateInfoXPred(_operationalMachine.X); // Обновление информации о предикатах X

            StateMemory(Decoder()); // Обновление состояния памяти

            KC_T(_operationalMachine.X); // Комбинационная схема T
            var y = KC_Y(); // Комбинационная схема Y
            KC_D(); // Комбинационная схема D
            _operationalMachine.Tact(y); // Выполнение такта в операционном автомате

            // Отображение данных
            _mainForm.UpdateInfoRegister(_operationalMachine.B, _operationalMachine.Count, _operationalMachine.C);
            _mainForm.UpdateStateMemory(_a);
            _mainForm.UpdateInfoKC(_t, y, _d, _operationalMachine.X);

            // Логика для выхода из имитационного устройства
            if (!_operationalMachine.X[0]) _end = true; // Если X0 становится ложным, устанавливаем _end в true
            if (_t[1] || _t[2] || _t[3] || _t[4] || _t[5] || _t[6]) _operationalMachine.X[0] = false; // Если X1, X2, X3, X4, X5 или X6 истинно, устанавливаем X0 в false
        }

        #region KC

        /// Комбинационная схема T (терм)
        private void KC_T(bool[] x)
        {
            // Инициализируем все термы в false
            for (var i = 0; i < _t.Length; i++)
                _t[i] = false;

            // Установка значений терм
            _t[0] = _a[0] && !x[0];
            _t[1] = _a[0] && x[0] && x[1];
            _t[2] = _a[0] && x[0] && !x[1] && x[2];
            _t[3] = _a[3] && x[7] && !x[8] && x[9];
            _t[4] = _a[3] && x[7] && !x[8] && !x[9];
            _t[5] = _a[4] && x[9];
            _t[6] = _a[4] && !x[9];
            _t[7] = _a[0] && x[0] && !x[1] && !x[2];
            _t[8] = _a[1] && !x[3] && x[4];
            _t[9] = _a[1] && !x[3] && !x[4] && x[5];
            _t[10] = _a[1] && !x[3] && !x[4] && !x[5];
            _t[11] = _a[3] && !x[7] && !x[3] && x[4];
            _t[12] = _a[3] && !x[7] && !x[3] && !x[4] && x[5];
            _t[13] = _a[3] && !x[7] && !x[3] && !x[4] && !x[5];
            _t[14] = _a[1] && x[3] && x[6];
            _t[15] = _a[1] && x[3] && !x[6];
            _t[16] = _a[2] && x[6];
            _t[17] = _a[2] && !x[6];
            _t[18] = _a[3] && !x[7] && x[3] && x[6];
            _t[19] = _a[3] && !x[7] && x[3] && !x[6];
            _t[20] = _a[3] && x[7] && x[8];
        }

        /// Комбинационная схема D
        private void KC_D()
        {
            _d[0] = _d[1] = _d[2] = false; // Инициализируем выходные данные из D-триггеров в false
            if (_t[7] || _t[14] || _t[15] || _t[16] || _t[17] || _t[18] || _t[19]) _d[0] = true; // Установка D0
            if (_t[8] || _t[9] || _t[10] || _t[11] || _t[12] || _t[13] || _t[14] || _t[15] || _t[16] || _t[17] ||
                _t[18] || _t[19]) _d[1] = true; // Установка D1
            if (_t[20]) _d[2] = true; // Установка D2
        }

        /// Комбинационная схема Y
        private bool[] KC_Y()
        {
            var y = new bool[13]; // Массив для хранения значений Y

            if (_t[1] || _t[2]) y[0] = true; // Y0
            if (_t[3] || _t[5]) y[12] = true; // Y12
            if (_t[7]) // Y1, Y2, Y3, Y4
            {
                y[0] = true;
                y[1] = true;
                y[2] = true;
                y[3] = true;
            }

            if (_t[8]) y[4] = true; // Y4
            if (_t[9]) y[5] = true; // Y5
            if (_t[10] || _t[13]) // Y6, Y7
            {
                y[6] = true;
                y[7] = true;
            }

            if (_t[11]) y[4] = true; // Y4
            if (_t[12]) y[5] = true; // Y5
            if (_t[14] || _t[16] || _t[18]) y[8] = true; // Y8
            if (_t[15] || _t[17] || _t[19]) // Y9, Y10, Y11
            {
                y[8] = true;
                y[9] = true;
                y[10] = true;
            }

            if (_t[20]) y[11] = true; // Y11

            return y; // Возвращаем массив Y
        }

        /// Дешифратор
        private short Decoder()
        {
            short number = 0; // Счетчик для декодирования

            if (_d[0])
                number = 1; // Если D0 истинно, устанавливаем number в 1
            if (_d[1])
                number += 2; // Если D1 истинно, увеличиваем number на 2
            if (_d[2])
                number += 4; // Если D2 истинно, увеличиваем number на 4

            return number; // Возвращаем декодированное значение
        }

        private short _predNumber; // Предыдущее состояние

        /// Память состояний
        private void StateMemory(short numberFromDTrigger)
        {
            _a[_predNumber] = false; // Сбрасываем предыдущее состояние
            _predNumber = numberFromDTrigger; // Сохраняем текущее состояние
            _a[numberFromDTrigger] = true; // Устанавливаем текущее состояние в true
        }

        #endregion

        /// Метод для сброса данных в автомате
        public void Reset()
        {
            A = 0; // Сброс числа A
            B = 0; // Сброс числа B
            _operationalMachine = new OperationalMachine(0, 0); // Инициализация операционного автомата
            InstallData = true; // Установка флага внесения данных
            _t = new bool[21]; // Инициализация массива терм
            _a = new bool[5]; // Инициализация массива меток состояний
            _a[0] = true; // Установка начальной метки состояния
            _d = new bool[3]; // Инициализация массива выходных данных из D-триггеров
            _end = false; // Сброс флага выхода из цикла
        }

        /// Операционный автомат
        public class OperationalMachine
        {
            /// Регистр A для хранения числа A
            private readonly ushort _a;

            /// Регистр B для хранения числа B
            public ushort B { get; private set; }

            /// Регистр C для хранения числа C
            public uint C { get; private set; }

            private byte _d; // Дополнительный регистр D

            public readonly bool[] X; // Массив условий X

            private readonly Action[] _yArray; // Массив действий Y

            /// Счетчик для управления циклом
            public byte Count { get; private set; }

            public OperationalMachine(ushort a, ushort b) // Конструктор класса OperationalMachine
            {
                _a = a; // Инициализация регистра A
                B = b; // Инициализация регистра B

                X = new bool[10]; // Инициализация массива условий X
                X[0] = true; // X0 всегда истинно

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

            /// Такт в операционном автомате
            public void Tact(bool[] signal)
            {
                for (var i = 0; i < _yArray.Length; i++) // Перебор массива действий Y
                {
                    if (signal[i]) // Если i-тый сигнал истинный
                    {
                        _yArray[i](); // Выполняем i-тое действие
                    }
                }

                LogicalDevice(); // Выполнение логического устройства
            }

            /// <summary>
            /// Логическое устройство.
            /// </summary>
            /// <returns>Массив условий X</returns>
            private void LogicalDevice()
            {
                X[1] = (_a & 0x7fff) == 0; // X1: A & 0x7fff == 0
                X[2] = (B & 0x7fff) == 0; // X2: B & 0x7fff == 0
                X[3] = (B & 0x0003) == 0; // X3: B & 0x0003 == 0
                X[4] = (B & 0x0003) == 1; // X4: B & 0x0003 == 1
                X[5] = (B & 0x0003) == 2; // X5: B & 0x0003 == 2
                X[6] = Count == 1; // X6: Count == 1
                X[7] = Count == 0; // X7: Count == 0
                X[8] = C << 16 >> 31 != 0; // X8: C << 16 >> 31 != 0
                X[9] = ((_a >> 15) ^ _d) == 1; // X9: (_a >> 15) ^ _d == 1
            }
        }
    }
}