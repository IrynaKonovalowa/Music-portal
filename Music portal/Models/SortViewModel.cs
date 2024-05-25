namespace Music_portal.Models
{
    public class SortViewModel
    {
        public SortState TitleSort { get; set; } // значение для сортировки по названию
        public SortState SingerSort { get; set; }    // значение для сортировки по исполнителю
        public SortState GenreSort { get; set; }    // значение для сортировки по жанру
        public SortState YearSort { get; set; }   // значение для сортировки по году
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            TitleSort = SortState.TitleAsc;
            SingerSort = SortState.SingerAsc;
            GenreSort = SortState.GenreAsc;
            YearSort = SortState.YearAsc;
            Up = true;

            if (sortOrder == SortState.TitleDesc || sortOrder == SortState.SingerDesc
                || sortOrder == SortState.GenreDesc || sortOrder == SortState.YearDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.TitleDesc:
                    Current = TitleSort = SortState.TitleAsc;
                    break;
                case SortState.SingerAsc:
                    Current = SingerSort = SortState.SingerDesc;
                    break;
                case SortState.SingerDesc:
                    Current = SingerSort = SortState.SingerAsc;
                    break;
                case SortState.GenreAsc:
                    Current = GenreSort = SortState.GenreDesc;
                    break;
                case SortState.GenreDesc:
                    Current = GenreSort = SortState.GenreAsc;
                    break;
                case SortState.YearAsc:
                    Current = YearSort = SortState.YearDesc;
                    break;
                case SortState.YearDesc:
                    Current = YearSort = SortState.YearAsc;
                    break;
                default:
                    Current = TitleSort = SortState.TitleDesc;
                    break;
            }
        }
    }
    //public class SortViewModel
    //{
    //    public SortState TitleSort { get; set; } // значение для сортировки по названию
    //    public SortState SingerSort { get; set; }    // значение для сортировки по исполнителю
    //    public SortState GenreSort { get; set; }    // значение для сортировки по жанру
    //    public SortState YearSort { get; set; }   // значение для сортировки по году
    //    public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
    //    public bool Up { get; set; }  // Сортировка по возрастанию или убыванию
    //    public SortViewModel(SortState sortOrder)
    //    {
    //        // значения по умолчанию
    //        TitleSort = SortState.TitleAsc;
    //        SingerSort = SortState.SingerAsc;
    //        GenreSort = SortState.GenreAsc;
    //        YearSort = SortState.YearAsc;


    //        TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
    //        SingerSort = sortOrder == SortState.SingerAsc ? SortState.SingerDesc : SortState.SingerAsc;
    //        GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
    //        YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
    //        Current = sortOrder;
    //    }
}

