using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<Книги> BookShopList = Classes.BaseClass.BookShopBase.Книги.ToList(); //Создание списка книг
        public AdminPage()
        {
            InitializeComponent();
            DataGridTable.ItemsSource = BookShopList; //Указание таблице брать данные из списка книг
        }
        int i = -1;
        int indexChange;
        private void MediaElement_Initialized(object sender, EventArgs e) //Инициализация изображений
        {
            if (i < BookShopList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Книги S = BookShopList[i];
                Uri U = new Uri(S.Обложка, UriKind.RelativeOrAbsolute);
                ME.Source = U;
                //   i++;
            }
        }
        private void TextBlock_Initialized(object sender, EventArgs e) //Инициализация текстблоков с названием
        {
            if (i < BookShopList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Книги S = BookShopList[i];
                TB.Text = "Название: " + S.Название;
                //  i++;
            }

        }

        private void Price_Initialized(object sender, EventArgs e) //Инициализация текстблоков с ценой
        {
            TextBlock TB = (TextBlock)sender;
            Книги S = BookShopList[i];
            TB.Text = "Цена: " + Convert.ToInt32(S.Цена) + " руб.";
        }

        private void autor_Initialized(object sender, EventArgs e) //Инициализация текстблоков с именем автора
        {
            TextBlock TB = (TextBlock)sender;
            Книги S = BookShopList[i];
            TB.Text = "Автор: " + Convert.ToString(S.Авторы.Автор);
        }

        private void nalichOnSklad_Initialized(object sender, EventArgs e) //Инициализация текстблоков с количеством книг на складе
        {
            TextBlock TB = (TextBlock)sender;
            Книги S = BookShopList[i];
            if (S.Количество_склад > 5)
            {
                TB.Text = "Наличие на складе: " + "много";
            }
            else
            {
                TB.Text = "Наличие на складе: " + Convert.ToInt32(S.Количество_склад) + " штук";
            }
        }


        private void nalichOnShop_Initialized(object sender, EventArgs e) //Инициализация текстблоков с количеством книг в магазине
        {
            TextBlock TB = (TextBlock)sender;
            Книги S = BookShopList[i];
            if (S.Количество_магазин > 5)
            {
                TB.Text = "Наличие на складе: " + "много";
            }
            else
            {
                TB.Text = "Наличие в магазине: " + Convert.ToInt32(S.Количество_магазин) + " штук";
            }
        }
        int count__books = 0; //Счетчик кол-ва книг
        double price__sum = 0; //Счетчик цены за выбранные книги
        double count__sum = 0;
        double discount__price = 0; //Счетчик скидки
        private void Add__book_Click(object sender, RoutedEventArgs e) //Метод добавления книг в корзину
        {
            Button Add__book = (Button)sender;
            indexChange = Convert.ToInt32(Add__book.Uid);
            Книги S = BookShopList[indexChange];
            int price = S.Цена;
            count__books += 1;
            price__sum += price;
            count__cart.Text = "Количество выбранных книг: " + count__books;          

            if (count__books < 3)
            {
                discount__price = 0;
/*                count__sum = price__sum / 500;
                Math.Round(count__sum);
                if (count__sum >= 1)
                {
                    discount__price =  price__sum * (count__sum / 100);
                    Convert.ToInt32(discount__price);
                    discount__cart.Text = "Скидка составила: " + discount__price + " рублей";
                }
                else
                {
                    discount__cart.Text = "Скидка составила: " + discount__price + " рублей";
                }*/

                price__sum = price__sum - (discount__price);
                discount__cart.Text = "Скидка составила: " + discount__price + " рублей";
                price__cart.Text = "Цена покупки: " + price__sum + " рублей";

            }

            if (count__books >= 3 && count__books < 5)
            {
                discount__price = 0.05;
                discount__cart.Text = "Скидка составила: " + price__sum * discount__price + " рублей";
                price__sum = price__sum - (price__sum * discount__price);
                price__cart.Text = "Цена покупки: " + price__sum + " рублей";        
            }
            else if (count__books >= 5 && count__books < 10)
            {
                discount__price = 0.1;
                discount__cart.Text = "Скидка составила: " + price__sum * discount__price + " рублей";
                price__sum = price__sum - (price__sum * discount__price);
                price__cart.Text = "Цена покупки: " + price__sum + " рублей";
            }
            else if (count__books >= 10)
            {
                discount__price = 0.15;
                discount__cart.Text = "Скидка составила: " + price__sum * discount__price + " рублей";
                price__sum = price__sum - (price__sum * discount__price);
                price__cart.Text = "Цена покупки: " + price__sum + " рублей";
            }
        }

        private void Add__book_Initialized(object sender, EventArgs e) //Инициализация кнопки добавления книг в корзину
        {
            Button BNew = (Button)sender;
            if (BNew != null)
            {
                BNew.Uid = Convert.ToString(i);
            }
        }
    }
}
