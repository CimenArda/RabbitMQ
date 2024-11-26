# RabbitMQ
Bu repo, RabbitMQ ile yapılan uygulamalara dair temel  mesajlaşma senaryolarını kapsamaktadır. Aşağıdaki başlıklar altında yapılan çalışmaları içerir:

RabbitMQ Kurulum ve Temel Kullanım: RabbitMQ'nun temel özellikleri ve kurulum süreci ile ilgili örnekler bulunmaktadır. Publisher ve Consumer uygulamaları üzerinden mesajlaşma altyapısının nasıl işlediği gösterilmektedir.

Exchange Türleri ve Kullanımları: RabbitMQ'nun farklı exchange türleri (Direct, Fanout, Header, Topic) ve bunların kullanım senaryoları açıklanmış ve örneklerle gösterilmiştir.

Mesajlaşma Tasarımları: Mesajlaşma altyapısında kullanılan çeşitli tasarımlar (Publish-Subscribe, Work Queue, Point-to-Point, Request-Response) üzerinde çalışılmıştır. Bu tasarımlar, sistemdeki farklı mesaj akışlarını nasıl yönlendireceğimizi ve iş yükünü nasıl dağıtacağımızı anlatmaktadır.

MassTransit ve ESB Modeli: RabbitMQ'yu Enterprise Service Bus (ESB) modeline entegre etme konusunda MassTransit kullanılarak örnekler verilmiştir. Bu sayede daha büyük ve dağıtık sistemlerde mesajlaşma stratejileri sunulmuştur.

## İçindekiler
- [BasicLevelPublisherAndConsumerApp](#basiclevelpublisherandconsumerapp)
  - [Uygulama Mimarisi](#uygulama-mimarisi)
  - [Publisher Tarafı](#publisher-tarafı)
  - [Consumer Tarafı](#consumer-tarafı)
  - [Exchange Types](#exchange-types)
    - [Direct Exchange](#direct-exchange)
    - [Fanout Exchange](#fanout-exchange)
    - [Header Exchange](#header-exchange)
    - [Topic Exchange](#topic-exchange)
  - [MessageDesigns](#messagedesigns)
    - [Publish-Subscribe Design](#publish-subscribe-design)
    - [Work Queue Design](#work-queue-design)
    - [Point-to-Point Design](#point-to-point-design)
    - [Request-Response Design](#request-response-design)
  - [EnterpriseServiceBus](#enterpriseservicebus)
  - [Request-ResponsePatternWithMassTransit](#request-responsepatternwithmasstransit)

## BasicLevelPublisherAndConsumerApp <a name="basiclevelpublisherandconsumerapp"></a>

Bu proje, RabbitMQ kullanarak temel bir Publisher ve Consumer uygulamasıdır. Proje, RabbitMQ üzerinden mesaj kuyruğuna mesaj göndermeyi ve kuyruktan mesaj tüketmeyi göstermektedir.

### Uygulama Mimarisi <a name="uygulama-mimarisi"></a>

#### Publisher (Mesaj Gönderici):
- Mesajları RabbitMQ kuyruğuna gönderir.

#### Consumer (Mesaj Tüketici):
- Kuyruktaki mesajları okur ve işler.

### Publisher Tarafı <a name="publisher-tarafı"></a>

1. **Bağlantı Oluşturma**: RabbitMQ bağlantısı `ConnectionFactory` sınıfı ile oluşturulur.
2. **Kanal Açma**: Bağlantı üzerinden bir kanal açılır.
3. **Queue Oluşturma**: Mesajlar için bir kuyruk tanımlanır.
4. **Mesaj Gönderme**: Kuyruğa bir mesaj (örneğin, Merhaba) gönderilir.

### Consumer Tarafı <a name="consumer-tarafı"></a>

1. **Bağlantı Oluşturma**: RabbitMQ bağlantısı `ConnectionFactory` sınıfı ile oluşturulur.
2. **Kanal Açma**: Bağlantı üzerinden bir kanal açılır.
3. **Queue Tanımlama**: Publisher'daki kuyruğun aynısı tanımlanır.
4. **Mesaj Tüketimi**: Kuyruktan gelen mesajlar `EventingBasicConsumer` ile işlenir.

## Exchange Types <a name="exchange-types"></a>

Bu proje, RabbitMQ'nun çeşitli Exchange Type türlerini uygulamalı olarak göstermektedir:  
DirectExchange, FanoutExchange, HeaderExchange, ve TopicExchange.

### Direct Exchange <a name="direct-exchange"></a>

**Neden Kullanılır?**  
Direct Exchange, belirli bir routing key ile birebir eşleşen mesajları yalnızca ilgili kuyruğa yönlendirmek için kullanılır. Bu yöntem, mesajların hedef kitleye spesifik olarak ulaştırılması gerektiği senaryolarda idealdir.

**Ne İşimize Yarar?**  
Bu tip exchange, mesajların yalnızca belirli bir kuyruğa gitmesini sağlar. Örneğin, bir sistemde kullanıcı rolleri bazında farklı kuyruklar oluşturmak ve mesajları bu rollere göre yönlendirmek istediğimizde kullanılır.

**Uygulamada Ne Yapıldı?**  
- Publisher (Gönderici) tarafında:
  - Bir Direct Exchange tanımlandı.
  - Kullanıcıdan alınan mesajlar, belirli bir routing key ile yayınlandı.
- Consumer (Tüketici) tarafında:
  - Aynı exchange ve routing key ile bir kuyruk bağlandı.
  - Gelen mesajlar kuyruktan okunarak ekrana yazdırıldı.

### Fanout Exchange <a name="fanout-exchange"></a>

**Neden Kullanılır?**  
Fanout Exchange, bir mesajın bağlı olan tüm kuyruklara yayınlanmasını sağlar. Bu yöntem, bir mesajın birden fazla tüketiciye aynı anda gönderilmesi gerektiğinde kullanılır.

**Ne İşimize Yarar?**  
Örneğin, birden fazla sisteme aynı anda bildirim göndermek istediğimiz durumlarda kullanılır. Log sistemleri, haberleşme altyapıları gibi dağıtık sistemlerde sıklıkla tercih edilir.

**Uygulamada Ne Yapıldı?**  
- Publisher (Gönderici) tarafında:
  - Bir Fanout Exchange tanımlandı.
  - Mesajlar, routing key kullanılmadan exchange üzerinden yayınlandı.
- Consumer (Tüketici) tarafında:
  - Kullanıcı tarafından verilen kuyruk adıyla kuyruk oluşturuldu.
  - Kuyruk, Fanout Exchange'e bağlandı ve mesajlar okunarak ekrana yazdırıldı.

### Header Exchange <a name="header-exchange"></a>

**Neden Kullanılır?**  
Header Exchange, mesaj yönlendirmesini routing key yerine, mesajın başlıklarına (header) göre yapar. Daha esnek ve dinamik bir yapı sunar.

**Ne İşimize Yarar?**  
Başlıklara dayalı özel filtreleme gerektiğinde kullanılır. Örneğin, mesajların kullanıcı kimlikleri veya sistem durumlarına göre dağıtılması gibi senaryolar için uygundur.

**Uygulamada Ne Yapıldı?**  
- Publisher (Gönderici) tarafında:
  - Bir Header Exchange tanımlandı.
  - Kullanıcıdan alınan bir değer, mesajın başlığına (header) eklendi.
  - Mesajlar bu başlık bilgisiyle yayınlandı.
- Consumer (Tüketici) tarafında:
  - Aynı başlık değeriyle bir kuyruk bağlandı.
  - Mesajlar bu başlık filtresine göre kuyruğa yönlendirilip tüketildi.

### Topic Exchange <a name="topic-exchange"></a>

**Neden Kullanılır?**  
Topic Exchange, mesaj yönlendirmesini joker karakterlerin de desteklendiği routing key'lere göre yapar. Esnek bir yönlendirme sağlar.

**Ne İşimize Yarar?**  
Örneğin, bir haber sistemi uygulamasında sports.football veya news.world gibi konularla filtreleme yaparak farklı türdeki içerikleri tüketmek için kullanılır.

**Uygulamada Ne Yapıldı?**  
- Publisher (Gönderici) tarafında:
  - Bir Topic Exchange tanımlandı.
  - Kullanıcıdan alınan bir topic routing key ile mesaj yayınlandı.
- Consumer (Tüketici) tarafında:
  - Kullanıcıdan alınan bir topic pattern ile bir kuyruk oluşturuldu.
  - Kuyruk, belirli bir topic pattern ile exchange'e bağlandı ve mesajlar tüketildi.

## MessageDesigns <a name="messagedesigns"></a>

Mesaj Tasarımları (Message Designs), farklı iletişim senaryolarında kullanılan yapı ve modelleri ifade eder. Bu tasarımlar, mesajlaşma sistemlerinin daha verimli, güvenilir ve ihtiyaca uygun çalışmasını sağlar.

### Publish-Subscribe Design <a name="publish-subscribe-design"></a>

**Neden Kullanılır?**  
Publish-Subscribe tasarımı, bir mesajın birden fazla alıcıya dağıtılmasını sağlar. Gönderici, mesajı bir exchange aracılığıyla yayınlar ve alıcılar (subscriber'lar) bu exchange'e bağlanır.

**Ne İşimize Yarar?**  
Bu tasarım, birden fazla sistemin aynı mesajı alıp işlemesi gereken durumlarda idealdir. Örneğin, bir haberleşme sisteminde bir olayın farklı modüller tarafından işlenmesi gerektiğinde kullanılır.

**Uygulamada Ne Yapıldı?**  
- Mesajlar bir Fanout Exchange aracılığıyla tüm kuyruklara yayınlandı.
- Her alıcı, exchange'deki mesajları farklı kuyruklarda tüketti.

### Work Queue Design <a name="work-queue-design"></a>

**Neden Kullanılır?**  
Work Queue, birden fazla işçinin (worker) aynı kuyruğa bağlı olduğu ve işleri bölüştüğü bir yapıdır. Bu, işlemlerin yük dengelemesi yapılarak gerçekleştirilmesini sağlar.

**Ne İşimize Yarar?**  
Yoğun iş yükü olan durumlarda, işlerin eşit bir şekilde dağıtılarak işlenmesini sağlar. Örneğin, bir resim işleme sisteminde gelen resimlerin farklı işleyicilere dağıtılması.

**Uygulamada Ne Yapıldı?**  
- Gönderici (publisher) bir kuyruk üzerinden mesaj gönderdi.
- Birden fazla işçi (consumer), aynı kuyruğa bağlanarak mesajları işledi.

### Point-to-Point Design <a name="point-to-point-design"></a>

**Neden Kullanılır?**  
Point-to-Point, bir gönderici (producer) ile bir alıcı (consumer) arasında birebir iletişim sağlar. Mesaj bir kuyruğa gönderilir ve yalnızca bir alıcı tarafından tüketilir.

**Ne İşimize Yarar?**  
Bu yapı, bir mesajın yalnızca tek bir tüketici tarafından alınmasının yeterli olduğu durumlarda idealdir. Örneğin, bir işlem sonrasında sadece bir kişiye bildirilen bir uyarı mesajı.

**Uygulamada Ne Yapıldı?**  
- Bir kuyruk oluşturuldu ve mesaj yalnızca bir consumer tarafından alındı.

### Request-Response Design <a name="request-response-design"></a>

**Neden Kullanılır?**  
Request-Response, bir sorgu yapılıp bir cevap alınan iletişim modelidir. Bu model, sistemler arasında etkileşimi sağlayan temel bir yapıdır.

**Ne İşimize Yarar?**  
Sunucu-istemci modelinde, istemcinin bir sorguya yanıt alması gerektiği durumlarda kullanılır. Örneğin, bir API çağrısında verilerin sorgulanması ve cevaplanması.

**Uygulamada Ne Yapıldı?**  
- Request-Response pattern MassTransit ile uygulandı. Gönderen, bir sorgu gönderdi ve alıcı bu sorguya yanıt verdi.

## EnterpriseServiceBus <a name="enterpriseservicebus"></a>

**Neden Kullanılır?**  
Enterprise Service Bus (ESB), mikro hizmetler arasında mesajlaşmayı kolaylaştıran bir yapıdır. ESB, farklı sistemlerin birbirleriyle iletişim kurabilmesini sağlayan bir altyapıdır.

**Ne İşimize Yarar?**  
ESB, hizmetlerin birbirleriyle veri ve mesaj alışverişinde bulunmasını sağlar. Farklı uygulamalar arasında veri entegrasyonu ve iş akışı yönetimini sağlar.

**Uygulamada Ne Yapıldı?**  
- MassTransit kullanılarak ESB tasarımı yapıldı.
- Uygulama, farklı sistemler arasında mesajlaşma yoluyla veri iletimini sağladı.

## Request-ResponsePatternWithMassTransit <a name="request-responsepatternwithmasstransit"></a>

**Neden Kullanılır?**  
MassTransit ile request-response pattern uygulamak, mikro hizmetler arasında senkron bir şekilde veri iletimi sağlar.

**Ne İşimize Yarar?**  
Mikro hizmetler arasında sorulara yanıt almak ve işlemleri senkronize bir şekilde yürütmek için kullanılır.

**Uygulamada Ne Yapıldı?**  
- MassTransit kullanarak request-response modelini implemente ettim.
