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
using System.Net;
using System.IO;
using System.Net.Http;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Autofac;
using Moq;
using Ploeh.AutoFixture;

namespace LabRegistrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Token postmantoken = new Token();
        // Contract contract = new Contract();
        // Response httpResp = new Response();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel();
        }
    }
}

#region Пример Контейнера, мока и автофилла

//        private void RegisterContainer()
//        {
//            ////Real
//            //INameProvider np = new NameProvider();
//            //var animal = new Cat(np);
//            //var animalInfo = animal.GetInfo();

//            //Moq

//            var fix = new Fixture();
//            var order = fix.Build<Order>().With(x=>x.Field1, "qwe").Create();
//            var info = order.GetSumm();

            

//            var builder = new ContainerBuilder();

//#if DEBUG
//            var moq = new Mock<INameProvider>();
//            moq.Setup(x => x.GetNameById(It.IsAny<int>())).Returns("Animal_test");
//            INameProvider np = moq.Object;

//            builder.RegisterInstance(np).As<INameProvider>();

//#else
//            builder.RegisterType<NameProvider>().As<INameProvider>();
//#endif





//            var container = builder.Build();

//            //Call
//            var p = container.Resolve<INameProvider>();
//            var animal = new Cat(p);
//            var animalInfo = animal.GetInfo();
//        }
//    }

//    public interface IAnimal
//    {
//        string GetInfo();
//    }

//    class Cat : IAnimal
//    {
//        private readonly INameProvider _np;

//        public Cat(INameProvider np)
//        {
//            _np = np;
//        }
//        public string GetInfo()
//        {
//            return $"Animal name is {_np.GetNameById(1)}";
//        }
//    }
//    public interface INameProvider
//    {
//        string GetNameById(int id);
//    }

//    class NameProvider : INameProvider
//    {
//        public string GetNameById(int id)
//        {
//            return $"Aimal_{id}";
//        }
//    }


//    public class Order
//    {
//        public string Field1 { get; set; }
//        public string Field2 { get; set; }
//        public string Field3 { get; set; }
//        public string Field4 { get; set; }
//        public string Field5 { get; set; }
//        public string Field11 { get; set; }
//        public string Field22 { get; set; }
//        public string Field21 { get; set; }
//        public string Field31 { get; set; }
//        public Invoice Invoice1 { get; set; }
//        public Invoice Invoice12 { get; set; }
//        public Invoice Invoice13 { get; set; }
//        public Invoice Invoice14 { get; set; }
//        public Invoice Invoice15 { get; set; }

//        public string GetSumm()
//        {
//            return
//                $"{Field1} {Field2} {Field3} {Field4} {Field5} {Field11} {Field22} {Field21} {Field31} {Invoice1?.Price} {Invoice12?.Price} {Invoice13?.Price} {Invoice14?.Price} {Invoice15?.Price} ";
//        }
//    }
//    public class Invoice
//    {
//        public int Price { get; set; }
//    }
//}

#endregion