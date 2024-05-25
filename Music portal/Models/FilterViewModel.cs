using Microsoft.AspNetCore.Mvc.Rendering;

namespace Music_portal.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Genre> genres, List<Singer> singers, int genre, /*string singer*/int singer)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            genres.Insert(0, new Genre { Name = "All", Id = 0 });
            Genres = new SelectList(genres, "Id", "Name", genre);
            SelectedGenre = genre;
            //SelectedSinger = singer;
            singers.Insert(0, new Singer { Name = "All", Id = 0 });
            Singers = new SelectList(singers, "Id", "Name", singer);
            SelectedSinger = singer;
        }
        public SelectList Genres { get; } // список жанров
        public int SelectedGenre { get; } // выбранный жанр
        public SelectList Singers { get; } // список исполнитель
        public int SelectedSinger { get; } // выбранный исполнитель
        //public string SelectedSinger { get; } // введенный исполнитель
    }
}
