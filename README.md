SORU 1) Bir MVC Controller’ın rolü nedir ve nasıl çalışır? Özellikle ActionResult döndüren metodlar nasıl işlenir?

Controller, kullanıcının isteklerini işleyip uygun yanıtları üreterek uygulamanın iş mantığını yönetir. View ile Model arasındaki bağlantı kurulur, gelen requestler Controller'da değerlendirilir, işlemlerin sonucuna göre response döner. SOLID prensipleri açısından servisle kullanılması daha doğrudur. Controller'da DB'ye erişmek yerine servislere istek atmak ve ilgili işlemleri servis aracılığı ile gerçekleştirmek kod tekrarını önleyecek okunabilirliği arttıracak ve daha güvenli olacaktır. Bu sayede kodun bakımı da kolaylaşacaktır.

ActionResult, bir controller metodunun çalışmasının sonucunda elde edilen veriyi, uygun bir formatta kullanıcıya döndürmek için kullanılan bir mekanizmadır. ActionResult ile farklı türlerde response dönülebilir bu sayede aynı controller içinde farklı isteklere farklı türde yanıtlar verebilirsiniz. 

Kullanıcı arayüzde bir url'e istek atar, bu istek controller üzerindeki bir metodu tetikler. İlgili action iş mantığına göre bir ya da daha fazla servisi kullanarak veritabanı üzerinde işlemler yapabilir ve isteğin ActionResult tipinde response dönmesi sağlanır. Bu noktada eğer bir html sayfası dönülecekse ViewResult, JSON dönülecekse JsonResult, başka bir sayfaya yönlendirilecekse RedirectResult, herhangi bir file döndürelecekse FileResult kullanılır. sonuç olarak ActionResult, istekte bulunan tarayıcıya veya API istemcisine yanıt olarak iletilir.


----------------------------------------


SORU 2) State Management (Durum Yönetimi) Yöntemleri; ASP.NET MVC ve MVC Core’da Session, TempData, ViewData ve ViewBag arasındaki farklar nelerdir? Bu yöntemleri nasıl ve hangi durumlarda tercih edersiniz?

HTTP stateless bir protokoldür, yani her HTTP isteği bir önceki istek hakkında bilgi sahibi değildir. Bir sayfadan başka bir sayfaya yönlendirme yapıldığında, daha sonra erişebilmeniz için verilerin korunması veya kalıcı hale getirilmesi gerekir. ASP.NET MVC bir sayfadan diğer sayfaya yeniden yönlendirirken veya yeniden yüklemeden sonra aynı sayfada verileri korunmasına yardımcı olabilecek State Management teknikleri sağlar.


Session, kullanıcının tarayıcısına bağlı olarak sunucu tarafında verileri saklayan bir nesnedir. Genellikle cookie kullanarak Session ID taşır; verinin kendisi sunucu tarafında saklanır, bu nedenle daha güvenlidir. Büyük miktarda veri saklanabilir. Oturum sonlanana kadar ya da belirli bir süre boyunca geçerlidir. Kullanıcının oturum bilgilerini, yetki seviyesini ya da kullanıcıya özel(giriş bilgileri,alışveriş sepeti vb.) geçici verileri saklamak için idealdir Ancak Uygulama performansını etkileyebilir, ölçeklenebilirlik sorunlarına neden olabilir ve güvenlik açısından risk taşıyabilir (cookiesde saklandığı için).

TempData, yalnızca bir sonraki HTTP isteğine kadar verileri saklayan bir yapıdır. ASP.NET Core’da TempData, Session State üzerinde çalışır. Birden fazla sayfa arasında veri aktarımı sağlar. Sadece bir sonraki istek süresince geçerlidir. TempData.Keep() ya da TempData.Peek() kullanılarak verilerin birden fazla istek boyunca saklanması sağlanabilir. Genellikle bir işlem sonucunda kullanıcıya bilgi mesajları göstermek ya da yönlendirme sonrası verileri taşımak için kullanılır. Veriler sadece bir kere okunur, bu da güvenliği artırır. Kısa süreli veri taşıma işlemleri için idealdir.

ViewData, verileri Controller'dan View'a gönderirken verilerinizi korumamıza yardımcı olur. Bir dictionary nesnesidir ve ViewDataDictionary'den türetilmiştir. Bir sözlük nesnesi olduğu için verileri anahtar-değer çifti olarak alır. Veriler sadece bir istek süresince geçerlidir. View ve Partial View’ler arasında veri paylaşımı yapılabilir. Tip güvenli değildir; veri tipini belirtmek gerekir. Controller’dan view’e küçük miktarda geçici veri taşımak için uygundur. Örneğin, bir sayfa başlığı ya da basit mesajları taşımak için kullanılabilir.

ViewBag, ViewData'dan farklı olarak tip güvenli değildir, dinamik bir yapıya sahiptir. Bu yapısı sebebiyle ufak performans farklılıkları gösterebilir.

Session: Kullanıcıya özel verilerin oturum süresince saklanması gerekiyorsa kullanılır. Alışveriş sepeti, kullanıcı oturumu gibi durumlarda tercih edilir.
TempData: Sayfalar arası geçici veri aktarımı yapılacaksa, yönlendirme (redirect) sonrasında bile veri saklanması isteniyorsa tercih edilir.
ViewData ve ViewBag: Controller’dan view’e geçici veri taşınacaksa ve sadece aynı istekte kullanılacaksa bu yöntemler uygundur. ViewBag, dinamik bir nesne yapısı sunduğu için tercih edilebilir.


----------------------------------------


SORU 3) .NET Core MVC’de Dependency Injection’ın nasıl kullanıldığını ve hangi avantajları sunduğunu anlatabilir misiniz? Geleneksel .NET MVC’de bu yapı nasıl sağlanırdı?

Interface üzerinde metotlar tanımlanır. interface'te belirlenen yöntemler service'te uygulanılır. Startup.cs dosyasında, ConfigureServices metodu kullanılarak servis eklenir. bu işlem esnasında Transient, singleton ya da Scoped kullanılabilir. Her kullanımda tekrar oluşturmak için transient, bir kez oluşturmak ve her seferinde onu kullanmak için Singleton, istek sonlanana kadar gerekli tüm kullanımlarda bir kez oluşturmak için scoped kullanılır. Daha sonra bu servisi controller üzerinde kullanabilmek için constructer üzerinde servisin interface'i alınır. 

Dependency injection SOLID prensiplerini uygularken bağımlılıkların en aza indirmek için kullanılan bir yöntemdir. Dependency injection kullanımı ile beraber Sınıflar, bağımlılıkları dışarıdan aldığı için kolayca mock edilerek test edilebilir. Kod daha modüler ve anlaşılır hale gelir. Bir değişiklik yapıldığında etkileşimi az olan diğer kısımlar etkilenmez. Yeni özellikler eklemek veya mevcut özellikleri değiştirmek daha kolaydır. Loose Coupling sayesinde sistemde değişiklik yapmak daha güvenli hale gelir.

.NET MVC'de de Dependency Injection kullanmak mümkündü ancak framework tarafından bu kadar entegre bir şekilde desteklenmiyordu. Genellikle üçüncü parti araçlar veya kendi yazdığınız yapılandırmalar kullanılırdı. Bu da daha fazla çaba gerektiriyordu. Ninject gibi üçüncü parti IoC containerler kullanılarak yapılacağı gibi DependencyResolver sınıfını kullanarak kendi IoC container yapınızı oluşturabilirsiniz.


----------------------------------------


SORU 4) MVC Core projesini mikroservis mimarisine nasıl adapte ederdiniz? Bağlantılı servislerle iletişim kurarken hangi teknolojileri veya teknikleri kullanırdınız (örn. gRPC, RESTful API, Event-Driven)?


Bunun bir ERP projesi olduğunu varsayarak cevaplamak gerekirse ilk adım olarak, ERP sistemindeki farklı işlevleri (örneğin üretim, muhasebe, stok, nakliye) bağımsız mikroservislere bölerdim. Her bir modülü kendi başına çalışan birer .NET Core servisi olarak yapılandırırdım. Docker ve Kubernetes ile bu servisleri konteynerize ederek geliştirme ve dağıtım süreçlerini kolaylaştırır, ihtiyaca göre otomatik ölçeklendirme yaparak performans ve esneklik sağlarım.

Mikroservisler arasında düşük gecikme süresi ve hızlı veri transferi gerektiren durumlarda gRPC kullanmayı tercih ederdim. ERP projelerinde, sık veri alışverişi yapan modüller (örneğin üretim ve stok servisleri) arasında gRPC ile daha verimli ve performanslı bir iletişim sağlanabilir.

Tüm API trafiğini merkezi bir noktadan yönetmek için Ocelot gibi bir API Gateway çözümü kullanırdım. Bu sayede gelen istekleri mikroservislere yönlendirir, yük dengelemesi yapar ve performansı artırabilirim. Ayrıca, API’lerin izlenmesi ve güvenlik yönetimi de kolaylaşır.

Mikroservisler arasında veri tutarlılığını sağlamak için RabbitMQ gibi bir message broker kullanırım. Örneğin, üretim tamamlandığında stok servisine otomatik bir güncelleme mesajı gönderilir. Bu, olası bir hata durumunda sistemin tamamının etkilenmesini önler ve bağımsız modüllerin işlevselliğini korur.

ERP projelerinde okuma ve yazma işlemlerinin performansını artırmak için CQRS desenini uygularım. Yazma işlemleri için ilişkisel bir veritabanı kullanırken, okuma işlemlerinde daha hızlı veri erişimi sağlamak amacıyla NoSQL veritabanlarını tercih ederim. Özellikle stok ve sipariş yönetimi gibi yoğun okuma işlemi gerektiren alanlarda, bu yapı performansı ciddi ölçüde iyileştirir. Ayrıca, event sourcing ile yapılan değişiklikleri olay bazında kaydederek okuma modelinin güncel kalmasını sağlarım. Böylece, bakım ve geliştirme süreçlerinde sistemin sadece ilgili modülleri etkilenir, olası hataların tüm sistemi etkilemesi önlenir.


----------------------------------------


SORU 5) Aşağıdaki Test isimli Controller Aksiyonu için ekrana “merhaba” yazacak ViewResult döndürmenizi ve hata durumunda Views klasörü altındaki Shared klasörü içerisinde bulunduğunu varsaydığımız “hata.cshtml” dosyasını PartialViewResult ile döndürmenizi istiyoruz.
public ActionResult Test()
{
try
{
    // throw new Exception();

    ViewBag.Message = "Merhaba";
    return View();
}
catch
{
    return PartialView("Hata");
}
}

