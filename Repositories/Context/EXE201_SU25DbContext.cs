using BusinessObject;
using Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Context
{
    public class EXE201_SU25DbContext : IdentityDbContext<Users, Roles, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public EXE201_SU25DbContext()
        {

        }
        public EXE201_SU25DbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Blogs> Blogs => Set<Blogs>();
        public virtual DbSet<BlogTypes> BlogTypes => Set<BlogTypes>();
        public virtual DbSet<Brands> Brands => Set<Brands>();
        public virtual DbSet<Comments> Comments => Set<Comments>();
        public virtual DbSet<RecycleGuides> RecycleGuides => Set<RecycleGuides>();
        public virtual DbSet<RecycleLocations> RecycleLocations => Set<RecycleLocations>();
        public virtual DbSet<Wastes> Wastes => Set<Wastes>();
        public virtual new DbSet<Roles> Roles => Set<Roles>();
        public virtual new DbSet<Users> Users => Set<Users>();
        public virtual DbSet<WasteTypes> WatseTypes => Set<WasteTypes>();
        public virtual DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public virtual DbSet<Campaigns> Campaigns => Set<Campaigns>();
        public virtual DbSet<Bookings> Bookings => Set<Bookings>();
        public virtual DbSet<TransactionLogs> TransactionLogs => Set<TransactionLogs>();
        public virtual DbSet<TransactionTypes> TransactionTypes => Set<TransactionTypes>();

        public virtual DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();

            builder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Roles>().HasData(
                new Roles
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "User",
                    NormalizedName = "USER",
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                },
                new Roles
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                },
                new Roles
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                }
            );

            builder.Entity<Users>().HasData(
                new Users
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserName = "hoangminh",
                    FullName = "Phạm Hoàng Minh",
                    Address = "12 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh",
                    Gender = "Male",
                    NormalizedUserName = "HOANGMINH",
                    Email = "hoangminh@example.com",
                    NormalizedEmail = "HOANGMINH@EXAMPLE.COM",
                    PhoneNumber = "0901-234-567",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<Users>().HashPassword(null, "123@123"),
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                    RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Users
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserName = "linhanh",
                    FullName = "Nguyễn Lĩnh Anh",
                    Address = "45 Lý Thường Kiệt, Quận Hoàn Kiếm, Hà Nội",
                    Gender = "Female",
                    NormalizedUserName = "LINHANH",
                    Email = "linhanh@example.com",
                    NormalizedEmail = "LINHANH@EXAMPLE.COM",
                    PhoneNumber = "0912-345-678",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<Users>().HashPassword(null, "123@123"),
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Users
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    UserName = "ducthien",
                    FullName = "Trần Đức Thiện",
                    Address = "101 Trần Hưng Đạo, Quận Ninh Kiều, Cần Thơ",
                    Gender = "Male",
                    NormalizedUserName = "DUCTHIEN",
                    Email = "ducthien@example.com",
                    NormalizedEmail = "DUCTHIEN@EXAMPLE.COM",
                    PhoneNumber = "0987-654-321",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<Users>().HashPassword(null, "123@123"),
                    CreatedBy = "System",
                    CreatedTime = CoreHelper.SystemTimeNow,
                    LastUpdatedTime = CoreHelper.SystemTimeNow,
                    RoleId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                }
            );


            builder.Entity<WasteTypes>().HasData(
                new WasteTypes
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    TypeName = "Rác nhựa",
                    Description = "Rác thải từ nhựa không phân hủy",
                    IconUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fracnhua.png?alt=media"

                },
                new WasteTypes
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    TypeName = "Rác hữu cơ",
                    Description = "Rác có thể phân hủy sinh học như thức ăn thừa, lá cây",
                    IconUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Frachuuco.png?alt=media"
                },
                new WasteTypes
                {
                    Id = "33333333-3333-3333-3333-333333333333",
                    TypeName = "Rác kim loại",
                    Description = "Rác từ kim loại như sắt, nhôm, đồng",
                    IconUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Frackimloai.png?alt=media"
                },
                new WasteTypes
                {
                    Id = "44444444-4444-4444-4444-444444444444",
                    TypeName = "Rác điện tử",
                    Description = "Thiết bị điện tử hỏng như điện thoại, pin",
                    IconUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fracdientu.png?alt=media"
                },
                new WasteTypes
                {
                    Id = "55555555-5555-5555-5555-555555555555",
                    TypeName = "Rác thủy tinh",
                    Description = "Chai lọ thủy tinh, kính vỡ",
                    IconUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fracthuytinh.jpg?alt=media"
                }
            );

            builder.Entity<Wastes>().HasData(
                new Wastes
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Chai nhựa",
                    Description = "Chai nhựa PET thường dùng để đựng nước giải khát.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fchainhua.png?alt=media",
                    WasteTypeId = "11111111-1111-1111-1111-111111111111"
                },
                new Wastes
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "Túi nylon",
                    Description = "Túi nylon khó phân hủy thường dùng trong siêu thị.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Ftuinylon.jpg?alt=media",
                    WasteTypeId = "11111111-1111-1111-1111-111111111111"
                },
                new Wastes
                {
                    Id = "33333333-3333-3333-3333-333333333333",
                    Name = "Đồ ăn thừa",
                    Description = "Thức ăn thừa sau bữa ăn có thể dùng ủ phân.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fdoanthua.png?alt=media",
                    WasteTypeId = "22222222-2222-2222-2222-222222222222"
                },
                new Wastes
                {
                    Id = "44444444-4444-4444-4444-444444444444",
                    Name = "Lá cây rụng",
                    Description = "Lá cây khô có thể phân hủy tự nhiên.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Flacayrung.png?alt=media",
                    WasteTypeId = "22222222-2222-2222-2222-222222222222"
                },
                new Wastes
                {
                    Id = "55555555-5555-5555-5555-555555555555",
                    Name = "Lon nước ngọt",
                    Description = "Lon nhôm có thể tái chế để tiết kiệm tài nguyên.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Flonnuocngot.jpg?alt=media",
                    WasteTypeId = "33333333-3333-3333-3333-333333333333"
                },
                new Wastes
                {
                    Id = "66666666-6666-6666-6666-666666666666",
                    Name = "Pin cũ",
                    Description = "Pin cũ chứa kim loại nặng, cần xử lý đúng cách.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fpincu.png?alt=media",
                    WasteTypeId = "44444444-4444-4444-4444-444444444444"
                },
                new Wastes
                {
                    Id = "77777777-7777-7777-7777-777777777777",
                    Name = "Điện thoại hỏng",
                    Description = "Thiết bị điện tử không còn sử dụng được, cần tái chế.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fdienthoaihong.jpg?alt=media",
                    WasteTypeId = "44444444-4444-4444-4444-444444444444"
                },
                new Wastes
                {
                    Id = "88888888-8888-8888-8888-888888888888",
                    Name = "Chai thủy tinh",
                    Description = "Chai thủy tinh có thể rửa sạch và tái sử dụng.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fchaithuytinh.jpg?alt=media",
                    WasteTypeId = "55555555-5555-5555-5555-555555555555"
                }
            );

            builder.Entity<RecycleGuides>().HasData(
                 new RecycleGuides
                 {
                     Id = Guid.NewGuid().ToString("N"),
                     Title = "Cách phân loại chai nhựa PET",
                     Content = @"Chai nhựa PET (Polyethylene Terephthalate) là một trong những loại rác thải phổ biến nhất trong cuộc sống hàng ngày, đặc biệt là trong các sản phẩm nước giải khát, nước suối, dầu ăn, và các loại nước đóng chai khác. Việc phân loại đúng chai nhựa PET không chỉ giúp giảm gánh nặng cho môi trường mà còn tạo điều kiện thuận lợi cho quá trình tái chế, tái sử dụng tài nguyên. Dưới đây là hướng dẫn chi tiết và toàn diện về cách phân loại và xử lý chai nhựa PET một cách khoa học, hiệu quả và an toàn.
                        ### 1. Nhận diện chai nhựa PET
                        Chai nhựa PET thường có ký hiệu số 1 trong hình tam giác tái chế ở đáy chai. Đây là loại nhựa nhẹ, trong suốt, có khả năng chống thấm khí và hơi nước tốt, thường được dùng một lần do đặc tính dễ bị biến chất nếu sử dụng lại ở nhiệt độ cao.

                        ### 2. Tại sao cần phân loại chai PET?
                        Việc phân loại giúp:
                        - **Tăng hiệu quả tái chế:** Khi chai PET được làm sạch và không bị lẫn tạp chất như nắp nhựa khác loại, nhãn dán, hay chất lỏng còn sót lại, quá trình tái chế sẽ diễn ra nhanh chóng và ít tiêu tốn năng lượng hơn.
                        - **Hạn chế ô nhiễm:** PET không phân hủy sinh học. Nếu bị vứt bừa bãi, chúng có thể tồn tại hàng trăm năm, ảnh hưởng đến đất, nước và sinh vật.
                        - **Tạo nguồn nguyên liệu tái chế:** PET có thể tái chế thành sợi polyester cho ngành dệt, vải, đồ nội thất, bao bì...

                        ### 3. Các bước phân loại chai PET đúng cách

                        #### Bước 1: Thu gom riêng chai PET
                        Hãy tách chai PET ra khỏi rác thải hữu cơ, rác y tế hay các loại rác khó phân hủy. Chỉ nên thu gom chai PET sạch, không bị lẫn tạp chất nguy hại.

                        #### Bước 2: Rửa sạch chai
                        Rửa sạch các chất còn sót bên trong như nước ngọt, sữa, dầu ăn. Không cần dùng xà phòng, chỉ cần nước sạch. Điều này giúp giảm mùi hôi và ngăn ngừa ruồi nhặng, vi khuẩn phát triển.

                        #### Bước 3: Gỡ nắp và nhãn
                        Nắp chai thường làm từ nhựa HDPE (số 2), khác loại với PET. Cần vặn nắp ra, phân loại riêng. Một số trạm tái chế có thể từ chối các chai còn nguyên nắp. Nhãn dán nên được bóc ra nếu có thể, để giảm ô nhiễm nhựa tái chế.

                        #### Bước 4: Làm dẹp chai
                        Bóp dẹp chai lại để tiết kiệm không gian chứa và dễ vận chuyển. Việc này cũng giúp tránh trường hợp chai bị thu hút côn trùng do còn hơi ẩm.

                        #### Bước 5: Phân loại theo màu
                        Nếu có điều kiện, phân loại riêng chai trong suốt và chai có màu (xanh, nâu...) vì chúng sẽ được xử lý khác nhau trong quá trình tái chế. Chai trong suốt thường có giá trị cao hơn.

                        #### Bước 6: Đóng gói hoặc mang đến điểm thu gom
                        Nếu ở khu dân cư có chương trình phân loại rác, hãy đặt vào đúng thùng rác nhựa. Nếu không có, bạn có thể đóng gói và mang đến các điểm thu gom phế liệu, trạm tái chế hoặc các chiến dịch thu gom rác nhựa.

                        ### 4. Những điều không nên làm
                        - Không bỏ chai PET còn nước hoặc thức uống vào thùng tái chế.
                        - Không đốt chai PET vì sẽ sinh ra khí độc như dioxin.
                        - Không lẫn chai PET với các loại nhựa PVC (số 3), vì có thể gây lỗi trong quy trình tái chế.

                        ### 5. Một số lưu ý thêm
                        - PET không nên tái sử dụng để đựng thức uống nóng vì có thể giải phóng chất độc hại như antimony.
                        - Chai PET có thể tái chế 100%, nhưng cần đảm bảo không bị nhiễm bẩn quá mức.
                        - Nếu bạn có số lượng lớn chai PET, hãy liên hệ với các tổ chức môi trường hoặc công ty tái chế để được hỗ trợ.

                        ### 6. Vai trò của cá nhân và cộng đồng
                        Từng hành động nhỏ trong phân loại rác của mỗi người đều góp phần làm sạch môi trường sống. Việc phân loại chai PET đúng cách không chỉ giúp ích cho hệ thống thu gom tái chế mà còn tạo thói quen tích cực cho thế hệ tương lai. Các tổ chức đoàn thể, trường học, doanh nghiệp cũng nên tổ chức tập huấn, tuyên truyền để nâng cao nhận thức cộng đồng.

                        ### 7. Một số mô hình hay tại Việt Nam
                        - Tại TP. Hồ Chí Minh, một số chung cư đã lắp đặt thùng phân loại riêng cho chai PET.
                        - Một số tổ chức như GreenHub, RecycleVN triển khai chiến dịch đổi chai nhựa lấy quà.
                        - Trường học và siêu thị cũng tham gia chương trình thu gom và tái chế bao bì PET.

                        ### Kết luận
                        Phân loại chai nhựa PET là hành động đơn giản nhưng mang lại giá trị to lớn. Chỉ cần rửa sạch, gỡ nắp, bóp dẹp và phân loại đúng nơi, bạn đã góp phần bảo vệ môi trường, tiết kiệm tài nguyên và xây dựng lối sống xanh. Hãy bắt đầu từ hôm nay và chia sẻ thông tin này đến mọi người xung quanh.",
                     ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fchainhua.png?alt=media",
                     VideoUrl = "https://www.youtube.com/watch?v=iN-0BbRJlYk",
                     WasteId = "11111111-1111-1111-1111-111111111111"
                 },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tái sử dụng túi nylon",
                    Content = @"Túi nylon là vật dụng quen thuộc trong sinh hoạt hàng ngày của người dân Việt Nam. Chúng xuất hiện ở mọi nơi: từ chợ truyền thống, siêu thị, hàng quán vỉa hè đến các cửa hàng tiện lợi. Tuy nhiên, túi nylon lại chính là một trong những loại rác thải gây hại nghiêm trọng nhất cho môi trường vì khả năng phân hủy cực kỳ chậm – có thể mất đến 500-1000 năm mới tan biến trong tự nhiên. Vì vậy, tái sử dụng túi nylon là một giải pháp thiết thực, đơn giản nhưng vô cùng hiệu quả giúp giảm áp lực rác thải nhựa ra môi trường.

                    ### I. Vì sao phải tái sử dụng túi nylon?

                    Việt Nam nằm trong nhóm các quốc gia có lượng rác thải nhựa cao nhất thế giới. Trong đó, túi nylon chiếm một tỷ lệ đáng kể vì người dân thường có thói quen sử dụng một lần rồi vứt bỏ. Những chiếc túi này sau khi thải ra môi trường sẽ:
                    - Làm nghẹt cống rãnh, gây ngập úng đô thị.
                    - Làm chết động vật biển khi chúng ăn nhầm túi nhựa tưởng là thức ăn.
                    - Gây ảnh hưởng sức khỏe nếu bị đốt bừa bãi, sinh ra khí độc như dioxin, furan.
                    - Lãng phí tài nguyên vì túi nylon được sản xuất từ dầu mỏ.

                    ### II. Những cách tái sử dụng túi nylon thông minh

                    1. **Dùng lại để đi chợ, mua hàng**
                       - Giữ lại các túi còn sạch để sử dụng cho lần mua sắm sau. Bạn có thể gấp gọn lại cho vào túi xách, balo để dùng khi cần thiết.

                    2. **Lót thùng rác**
                       - Thay vì dùng bao rác mới, hãy tận dụng túi nylon cũ để lót thùng rác trong nhà, văn phòng.

                    3. **Lưu trữ đồ vật**
                       - Dùng để đựng giày dép, quần áo cũ, các vật nhỏ như dây điện, ổ sạc… giúp ngăn nắp, gọn gàng.

                    4. **Chống trầy xước khi di chuyển**
                       - Bọc túi nylon quanh giày dép, đồ dùng dễ vỡ khi đi du lịch hoặc chuyển nhà.

                    5. **Sáng tạo đồ tái chế**
                       - Làm đồ handmade như ví, túi xách, thảm chùi chân, ghế từ túi nylon đan lại – vừa bảo vệ môi trường, vừa tạo giá trị thẩm mỹ.

                    6. **Làm vật dụng chăm sóc cây trồng**
                       - Dùng túi để bọc chậu cây khi trời mưa lớn, hoặc để giữ ẩm đất.

                    ### III. Làm thế nào để bảo quản túi nylon dùng lại?

                    - Chỉ giữ lại các túi sạch, không bị rách.
                    - Gấp gọn, xếp vào một chiếc hộp hoặc treo lên móc cố định.
                    - Tránh để túi tiếp xúc với ánh nắng trực tiếp để không bị giòn, mục.

                    ### IV. Những điều cần tránh

                    - Không dùng lại túi nylon đã đựng thực phẩm sống, chất tẩy rửa hóa học.
                    - Không để trẻ nhỏ nghịch túi nylon vì nguy cơ ngạt thở.
                    - Không vứt túi nylon lung tung dù đã sử dụng nhiều lần.

                    ### V. Đem túi nylon đến điểm thu gom

                    Nếu bạn có quá nhiều túi nylon không thể dùng lại được nữa, đừng vứt vào thùng rác chung. Thay vào đó:
                    - Gấp gọn, gom lại và mang đến các siêu thị có đặt thùng thu gom túi nylon.
                    - Tham gia chương trình đổi rác lấy quà, đổi túi nylon lấy điểm thưởng ở các tổ chức môi trường.
                    - Một số tổ chức phi lợi nhuận nhận gom túi nylon để làm nguyên liệu tái chế hoặc tạo sản phẩm thủ công.

                    ### VI. Mô hình hay tại Việt Nam

                    - **TP.HCM:** Các chuỗi siêu thị như Co.opmart, LotteMart, Big C triển khai chương trình “Nói không với túi nylon” – khách hàng được tặng điểm hoặc giảm giá khi mang túi cá nhân.
                    - **Đà Nẵng:** Tổ dân phố phát động phong trào “Gia đình không dùng túi nylon mới”.
                    - **Hà Nội:** Một số cửa hàng zero-waste như Refill Station, Go Eco sử dụng túi giấy và khuyến khích khách hàng mang túi vải.

                    ### VII. Vai trò của bạn

                    Mỗi túi nylon được dùng lại một lần là một cơ hội giảm bớt gánh nặng cho môi trường. Tái sử dụng túi nylon không đòi hỏi kỹ thuật cao, chỉ cần thay đổi thói quen. Hãy cùng gia đình, bạn bè xây dựng văn hóa tiêu dùng bền vững, hạn chế nhựa một lần.

                    ### Kết luận

                    Tái sử dụng túi nylon là một bước đi nhỏ nhưng mang lại tác động lớn. Hành động này không chỉ góp phần bảo vệ hành tinh, giảm thiểu ô nhiễm môi trường mà còn giúp mỗi cá nhân tiết kiệm chi phí. Hãy bắt đầu ngay hôm nay: giữ lại túi nylon sạch, dùng lại khi cần và lan tỏa thông điệp xanh đến cộng đồng!",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Ftuinylon.jpg?alt=media",
                    VideoUrl = "https://www.youtube.com/watch?v=yBNIRq-en-o",
                    WasteId = "22222222-2222-2222-2222-222222222222"
                },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Ủ phân từ cơm thừa",
                    Content = @"Trong đời sống hàng ngày, cơm thừa và các loại thực phẩm hữu cơ bị bỏ đi chiếm tỷ lệ đáng kể trong tổng lượng rác thải sinh hoạt. Thay vì để lượng thức ăn thừa này trở thành gánh nặng cho môi trường, chúng ta hoàn toàn có thể tận dụng để ủ phân hữu cơ – một giải pháp không chỉ giúp giảm thiểu rác thải mà còn mang lại nguồn phân bón tự nhiên cho cây trồng, không hóa chất, an toàn cho sức khỏe.

                    ### I. Tại sao nên ủ phân từ cơm thừa?

                    1. **Giảm rác thải hữu cơ**  
                       Theo thống kê, mỗi hộ gia đình Việt Nam trung bình thải ra 0.5 – 1 kg rác hữu cơ/ngày, trong đó có cơm, rau, thức ăn thừa. Nếu không được xử lý đúng cách, chúng sẽ phân hủy, bốc mùi, sinh ra khí nhà kính (methane), ảnh hưởng đến môi trường và sức khỏe cộng đồng.

                    2. **Tiết kiệm chi phí phân bón**  
                       Thay vì mua phân bón hóa học, bạn có thể tận dụng thức ăn thừa để tạo ra phân compost – một loại phân hữu cơ tự nhiên giúp cải tạo đất, cung cấp vi sinh vật có lợi.

                    3. **Tái tạo vòng đời thực phẩm**  
                       Thức ăn sau khi bị bỏ đi được tái sử dụng để nuôi cây trồng, tạo ra thực phẩm mới – một vòng tuần hoàn xanh, thân thiện với môi trường.

                    ### II. Những loại thực phẩm có thể dùng để ủ phân

                    - Cơm nguội, cháo thừa
                    - Vỏ trái cây, rau củ
                    - Bã cà phê, bã trà
                    - Bánh mì khô, vỏ trứng
                    - Thức ăn chín đã để nguội

                    **Lưu ý:** Không ủ thịt sống, dầu mỡ, cá vì dễ sinh vi khuẩn gây hại và mùi hôi.

                    ### III. Cách ủ phân hữu cơ đơn giản tại nhà

                    #### 1. Chuẩn bị vật dụng
                    - Một thùng nhựa có nắp đậy hoặc thùng xốp (có đục lỗ thoát khí).
                    - Rác hữu cơ (cơm, rau, vỏ trái cây).
                    - Vật liệu khô: tro, giấy vụn, lá khô, mùn cưa.
                    - EM (chế phẩm vi sinh) hoặc nấm Trichoderma (giúp phân hủy nhanh).

                    #### 2. Các bước thực hiện

                    **Bước 1: Xé nhỏ, cắt nhỏ rác hữu cơ**  
                    Giúp tăng diện tích tiếp xúc và đẩy nhanh quá trình phân hủy.

                    **Bước 2: Trộn đều với vật liệu khô**  
                    Lớp ẩm (cơm, thức ăn) xen kẽ lớp khô (lá, giấy) để hút ẩm, không gây thối rữa.

                    **Bước 3: Cho vào thùng và nén nhẹ**  
                    Không nén quá chặt để đảm bảo có đủ oxy cho vi sinh vật hoạt động.

                    **Bước 4: Tưới EM hoặc nấm phân giải**  
                    Có thể mua tại cửa hàng nông nghiệp hoặc online. Nếu không có, có thể dùng nước vo gạo lên men.

                    **Bước 5: Đậy nắp, để nơi thoáng mát**  
                    Tránh ánh nắng trực tiếp và mưa.

                    **Bước 6: Đảo đều mỗi 3-5 ngày**  
                    Giúp phân hủy đều và tránh úng nước.

                    **Bước 7: Sau 30-45 ngày**  
                    Phân sẽ hoai mục, có màu đen nâu, mùi đất, không còn mùi hôi. Lúc này có thể sử dụng.

                    ### IV. Sử dụng phân hữu cơ như thế nào?

                    - Trộn vào đất trước khi trồng cây.
                    - Rải xung quanh gốc cây để giữ ẩm, cung cấp dinh dưỡng.
                    - Trồng rau sạch tại nhà, chậu cây ban công, vườn nhỏ.

                    ### V. Lưu ý khi ủ phân tại nhà

                    - Luôn che kín thùng ủ để tránh côn trùng, chuột.
                    - Không đổ thức ăn còn nước, dầu vào thùng.
                    - Nếu có mùi lạ, kiểm tra độ ẩm và trộn thêm vật liệu khô.
                    - Không nên ủ quá nhiều cùng lúc, hãy ủ theo từng đợt nhỏ.

                    ### VI. Mô hình thành công ở Việt Nam

                    - **Quận 7, TP.HCM:** Người dân tự ủ phân tại nhà, sau 3 tháng có thể dùng bón cây cảnh và vườn rau.
                    - **Hà Nội:** Dự án “Chúng tôi chọn sống xanh” phát động hơn 500 hộ dân ủ phân tại nhà bằng thức ăn thừa.
                    - **Đà Nẵng:** Trường học sử dụng bã canteen để tạo phân cho khuôn viên cây xanh trong trường.

                    ### VII. Tác động tích cực

                    - Giảm tải cho bãi rác thành phố.
                    - Hạn chế rác thực phẩm thải ra sông, hồ.
                    - Lan tỏa thói quen sống xanh, tiết kiệm và bền vững.
                    - Tạo hoạt động gắn kết gia đình khi cùng nhau ủ và trồng cây.

                    ### Kết luận

                    Ủ phân từ cơm thừa là một hành động nhỏ nhưng vô cùng có ý nghĩa trong nỗ lực bảo vệ môi trường và sống xanh. Thay vì để thức ăn thừa trở thành rác, bạn có thể biến chúng thành nguồn tài nguyên quý giá cho cây trồng. Không cần quá cầu kỳ, chỉ cần chút thời gian và sự kiên nhẫn, bạn hoàn toàn có thể thực hiện tại nhà. Hãy bắt đầu từ hôm nay, vì một môi trường sạch đẹp và bền vững!",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fdoanthua.png?alt=media",
                    VideoUrl = null,
                    WasteId = "33333333-3333-3333-3333-333333333333"
                },

                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Xử lý lá cây rụng đúng cách",
                    Content = @"Lá cây rụng là hiện tượng tự nhiên xảy ra quanh năm, đặc biệt rõ rệt vào mùa thu hoặc mùa khô. Tuy nhiên, trong môi trường đô thị, việc xử lý lượng lớn lá rụng mỗi ngày có thể trở thành gánh nặng nếu không được thực hiện đúng cách. Nếu vứt bừa bãi hoặc đốt không kiểm soát, lá cây có thể gây ô nhiễm không khí, tắc nghẽn cống rãnh, và ảnh hưởng đến sức khỏe cộng đồng. Hướng dẫn dưới đây sẽ giúp bạn xử lý lá cây rụng một cách an toàn, thân thiện với môi trường và còn có ích cho cuộc sống.

                    ### I. Vì sao cần xử lý lá cây đúng cách?

                    1. **Tránh ô nhiễm không khí**
                       - Việc đốt lá cây ngoài trời tạo ra khói bụi và khí độc như CO2, CO, PM2.5 – nguyên nhân chính gây bệnh đường hô hấp và ô nhiễm môi trường.

                    2. **Ngăn ngừa tắc nghẽn hệ thống thoát nước**
                       - Lá rụng khi bị cuốn xuống cống có thể tích tụ, làm nghẹt cống, gây ngập úng vào mùa mưa.

                    3. **Hạn chế côn trùng, nấm mốc**
                       - Lá cây mục ướt là môi trường lý tưởng cho muỗi, gián, nấm và vi khuẩn phát triển.

                    4. **Tận dụng làm phân bón hữu cơ**
                       - Lá cây chứa nhiều chất hữu cơ, là nguyên liệu lý tưởng để tạo ra phân compost giàu dinh dưỡng cho cây trồng.

                    ### II. Các cách xử lý lá cây rụng hiệu quả

                    #### 1. Thu gom và phân loại

                    - Dùng chổi quét lá chuyên dụng để gom lại.
                    - Tránh dùng vòi nước xịt vì có thể đẩy lá xuống cống rãnh.
                    - Phân loại riêng lá cây sạch (không lẫn rác nhựa, kim loại) để tái sử dụng hoặc ủ phân.

                    #### 2. Ủ compost từ lá cây

                    **Bước 1:** Chuẩn bị thùng ủ hoặc hố ủ ngoài trời.  
                    **Bước 2:** Trộn lá cây với cỏ khô, vỏ trái cây, vỏ trứng, rác nhà bếp hữu cơ.  
                    **Bước 3:** Bổ sung nước giữ ẩm, đảo trộn định kỳ 5-7 ngày/lần.  
                    **Bước 4:** Sau 2–3 tháng, hỗn hợp sẽ phân hủy thành phân hữu cơ có mùi đất nhẹ.  
                    **Bước 5:** Sử dụng cho cây cảnh, rau, hoa kiểng.

                    **Lưu ý:** Không ủ lá có dấu hiệu bệnh, sâu, để tránh lây lan cho cây trồng.

                    #### 3. Tạo lớp phủ giữ ẩm (mulch)

                    Lá cây khô có thể được xé nhỏ và rải đều lên bề mặt đất quanh gốc cây để:
                    - Giữ ẩm cho đất.
                    - Ngăn cỏ dại phát triển.
                    - Bổ sung hữu cơ tự nhiên khi lá phân hủy từ từ.

                    #### 4. Tái chế sáng tạo

                    Một số cách tái sử dụng lá cây:
                    - Làm đồ thủ công trang trí như tranh ép lá, bookmark.
                    - Trang trí lễ hội, sự kiện với chủ đề thiên nhiên.
                    - Dùng lá khô ép vào giấy để làm bìa thiệp hoặc sổ tay.

                    #### 5. Đốt lá đúng nơi, đúng cách (chỉ khi bắt buộc)

                    - Đốt tại nơi quy định, có sự giám sát.
                    - Không đốt gần khu dân cư, bệnh viện, trường học.
                    - Nên rải tro sau khi đốt ra vườn để tận dụng làm phân bón kali tự nhiên.

                    ### III. Những điều nên tránh

                    - Không vứt lá rụng xuống cống, sông hồ.
                    - Không gom lá cùng với rác nhựa, rác nguy hại.
                    - Không đốt trong hẻm nhỏ, khu đông dân cư, đặc biệt trong thời điểm ô nhiễm không khí cao.

                    ### IV. Mô hình xử lý lá cây hiệu quả

                    - **TP. Huế:** Các công viên dùng máy nghiền lá để ủ compost ngay tại chỗ.
                    - **Đắk Lắk:** Hộ dân trồng cà phê gom lá rụng làm lớp phủ gốc, giữ độ ẩm mùa khô.
                    - **Hội An:** Các tuyến phố cổ gom lá cây rụng và ủ phân tại vườn cộng đồng.

                    ### V. Vai trò của cá nhân và cộng đồng

                    1. **Cá nhân:** Chủ động thu gom, xử lý, không đổ bỏ lá bừa bãi. Tận dụng cho việc trồng cây tại nhà.
                    2. **Cộng đồng:** Hợp tác với tổ dân phố hoặc trường học để thực hiện các mô hình phân loại rác hữu cơ, ủ phân tập thể hoặc làm khu vườn lá tái chế.

                    ### VI. Gợi ý thiết kế khu xử lý tại gia

                    - Dành một góc sân vườn để làm hố ủ lá cây.
                    - Dùng thùng nhựa lớn đục lỗ để làm thùng ủ di động.
                    - Trồng cây ngay trên lớp lá ủ sau khi đã phân hủy đủ thời gian.

                    ### Kết luận

                    Xử lý lá cây rụng đúng cách không chỉ là trách nhiệm bảo vệ môi trường mà còn là hành động thiết thực để tái tạo tài nguyên và phát triển nông nghiệp bền vững. Thay vì đốt bỏ gây ô nhiễm, bạn hoàn toàn có thể tận dụng lá cây để làm phân bón, lớp phủ đất hoặc sáng tạo thành các vật dụng hữu ích. Hãy hành động ngay hôm nay để biến mỗi chiếc lá rụng thành món quà cho thiên nhiên!",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Flacayrung.png?alt=media",
                    VideoUrl = null,
                    WasteId = "44444444-4444-4444-4444-444444444444"
                },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tái chế lon nước nhôm",
                    Content = @"Lon nước nhôm là loại bao bì phổ biến cho các sản phẩm nước giải khát như nước ngọt, bia, nước tăng lực… Nhờ tính nhẹ, dễ vận chuyển và khả năng tái chế cao, nhôm là một trong những vật liệu lý tưởng để tái chế. Tuy nhiên, nếu không được phân loại đúng cách, mỗi lon nhôm vứt ra môi trường sẽ cần đến hàng trăm năm để phân hủy. Việc tái chế lon nước nhôm không chỉ giúp tiết kiệm tài nguyên mà còn giảm thiểu rác thải và ô nhiễm môi trường.

                    ### I. Tại sao nên tái chế lon nhôm?
                    1. **Nhôm có thể tái chế 100%**  
                       Nhôm không bị mất đi chất lượng trong quá trình tái chế. Một lon nhôm có thể được tái chế vô số lần thành sản phẩm mới, tiết kiệm tới 95% năng lượng so với sản xuất nhôm nguyên sinh.

                    2. **Tiết kiệm tài nguyên thiên nhiên**  
                       Việc tái chế nhôm giúp giảm nhu cầu khai thác quặng bauxite – một quá trình tiêu tốn nước, điện và gây ảnh hưởng đến môi trường sống của sinh vật.

                    3. **Giảm khí nhà kính**  
                       Tái chế 1 tấn nhôm giúp giảm tới 9 tấn CO2 so với sản xuất nhôm mới. Một lon nhôm được tái chế có thể quay lại kệ hàng chỉ sau 60 ngày.

                    4. **Tạo giá trị kinh tế**  
                       Nhôm là loại rác có giá trị cao, được các cơ sở thu gom và tái chế ưa chuộng. Gom lon nhôm có thể giúp người dân tăng thu nhập và góp phần vào nền kinh tế tuần hoàn.

                    ### II. Hướng dẫn tái chế lon nước nhôm đúng cách

                    #### Bước 1: Thu gom riêng lon nhôm
                    - Tách riêng lon nhôm khỏi các loại rác hữu cơ hoặc rác nhựa.
                    - Không bỏ lon nhôm vào thùng rác chung.

                    #### Bước 2: Rửa sạch bên trong
                    - Tráng nước để loại bỏ cặn nước ngọt, bia còn sót lại.
                    - Việc này giúp tránh mùi hôi, ruồi nhặng và tăng giá trị tái chế.

                    #### Bước 3: Bóp dẹp lon
                    - Dùng tay hoặc chân bóp nhẹ lon để làm dẹp.
                    - Giúp tiết kiệm không gian lưu trữ và vận chuyển.

                    #### Bước 4: Gom vào bao riêng
                    - Dùng túi, thùng riêng để đựng lon nhôm.
                    - Khi đủ số lượng có thể mang đến điểm thu gom, bán ve chai hoặc gửi cho các chương trình tái chế.

                    ### III. Những điều cần lưu ý

                    - Không bỏ lẫn lon nhôm với lon sắt, vì dễ làm lẫn tạp chất khi tái chế.
                    - Không đốt lon nhôm, vì nhiệt độ cao có thể gây cháy nổ và giải phóng khí độc.
                    - Không để lon dính dầu mỡ, thức ăn vì sẽ làm giảm chất lượng nhôm tái chế.

                    ### IV. Những gì không phải là lon nhôm

                    Một số người nhầm lẫn giữa lon nhôm và lon sắt hoặc hộp thiếc. Để kiểm tra:
                    - Dùng nam châm: nhôm không bị hút, sắt thì bị hút.
                    - Quan sát màu sắc: nhôm sáng, nhẹ hơn và mềm dẻo hơn sắt.
                    - Nếu không chắc chắn, hãy để riêng và hỏi nhân viên thu gom.

                    ### V. Mô hình tái chế lon nhôm tại Việt Nam

                    1. **Các điểm thu mua ve chai**  
                       - Ở hầu hết các khu dân cư, lon nhôm là một trong những loại phế liệu có giá trị cao, thường được thu mua với giá từ 25.000 – 35.000đ/kg (tùy thời điểm).

                    2. **Chiến dịch đổi lon lấy cây xanh**  
                       - Một số trường học, tổ chức môi trường tổ chức chương trình thu gom lon nhôm đổi lấy cây xanh, quà tặng, nhằm nâng cao nhận thức cộng đồng.

                    3. **Doanh nghiệp tái chế**  
                       - Các công ty như Nhôm Hòa Phát, Nhôm Đông Á có tiếp nhận nhôm tái chế để sản xuất cửa, thanh nhôm, vật liệu xây dựng.

                    4. **Chung cư xanh**  
                       - Nhiều tòa nhà tại TP.HCM, Hà Nội có thùng phân loại riêng cho lon nhôm, giúp cư dân phân loại dễ dàng và thân thiện môi trường.

                    ### VI. Tận dụng lon nhôm trong cuộc sống

                    Ngoài tái chế công nghiệp, bạn có thể:
                    - Làm chậu trồng cây mini.
                    - Tạo đèn cắm nến handmade.
                    - Dựng kệ đựng bút, cắm hoa đơn giản.
                    - Dùng lon cắt thành mảnh nhỏ làm móc khóa, đồ trang trí.

                    **Lưu ý:** Nếu cắt lon nhôm, hãy cẩn thận để không bị đứt tay.

                    ### VII. Vai trò của bạn trong tái chế

                    - Tạo thói quen rửa sạch, phân loại đúng từ trong nhà.
                    - Dạy trẻ em nhận biết lon nhôm và phân biệt các loại rác tái chế.
                    - Chia sẻ với bạn bè, hàng xóm để cùng nhau thực hiện lối sống xanh.

                    ### Kết luận

                    Tái chế lon nước nhôm là một trong những hành động đơn giản, dễ thực hiện nhưng mang lại giá trị môi trường và kinh tế lớn. Chỉ cần bạn bắt đầu từ việc rửa sạch, bóp dẹp và phân loại đúng, bạn đã góp phần tiết kiệm tài nguyên, giảm khí thải và xây dựng một cộng đồng bền vững. Hãy cùng nhau hành động vì một Việt Nam xanh hơn!",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Flonnuocngot.jpg?alt=media",
                    VideoUrl = "https://www.youtube.com/shorts/HKhLW1sxIPo",
                    WasteId = "55555555-5555-5555-5555-555555555555"
                },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Xử lý pin cũ an toàn",
                    Content = @"Pin là nguồn năng lượng phổ biến trong các thiết bị điện tử như điều khiển từ xa, đồng hồ, đồ chơi, máy tính cầm tay, điện thoại, laptop… Tuy nhiên, khi không còn sử dụng được, pin trở thành một loại rác thải nguy hại với thành phần chứa nhiều kim loại nặng như chì, thủy ngân, cadmium… gây tác hại nghiêm trọng đến môi trường và sức khỏe con người nếu bị vứt bỏ bừa bãi. Việc xử lý pin cũ đúng cách là trách nhiệm thiết yếu trong lối sống xanh hiện đại.

                    ### I. Vì sao pin cũ là rác thải nguy hại?

                    1. **Chứa kim loại độc hại**
                       - Pin cũ có thể chứa thủy ngân (Hg), chì (Pb), cadmium (Cd) – là những chất cực độc, nếu rò rỉ ra môi trường có thể ngấm vào đất, nước, ảnh hưởng lâu dài đến hệ sinh thái.

                    2. **Không thể phân hủy sinh học**
                       - Pin mất hàng trăm năm để phân hủy hoàn toàn. Trong thời gian đó, chất độc trong pin có thể phát tán dần ra không khí, nước ngầm.

                    3. **Nguy cơ cháy nổ**
                       - Pin lithium-ion trong điện thoại, laptop có thể phát nổ nếu bị đâm thủng, va đập mạnh hoặc tiếp xúc với nhiệt độ cao.

                    4. **Gây hại cho sức khỏe**
                       - Kim loại nặng từ pin có thể gây tổn thương gan, thận, hệ thần kinh nếu con người tiếp xúc hoặc hít phải.

                    ### II. Những loại pin phổ biến trong đời sống

                    - **Pin AA, AAA** (dùng trong điều khiển, đồ chơi, chuột máy tính)
                    - **Pin tròn** (dùng cho đồng hồ đeo tay, máy tính mini)
                    - **Pin điện thoại di động**
                    - **Pin laptop, máy ảnh**
                    - **Pin sạc dự phòng**

                    Tất cả các loại pin này đều cần được xử lý riêng biệt, không vứt chung với rác sinh hoạt.

                    ### III. Cách xử lý pin cũ an toàn

                    #### 1. Thu gom riêng

                    - Không vứt pin vào thùng rác chung hoặc đổ xuống cống.
                    - Dùng chai nhựa, hộp nhựa có nắp đậy để đựng pin cũ tại nhà.
                    - Ghi chú “Pin cũ” trên vỏ ngoài để tránh nhầm lẫn với rác khác.

                    #### 2. Đưa đến điểm thu gom

                    - Mang pin đến các **điểm thu gom pin chuyên dụng** do cơ quan nhà nước, siêu thị hoặc tổ chức môi trường thiết lập.
                    - Một số nơi nhận pin cũ miễn phí:
                      - Siêu thị Co.opmart, Big C, VinMart.
                      - Trung tâm bảo hành điện tử.
                      - Một số trường học, UBND phường tổ chức “Thứ Bảy Xanh” hoặc “Đổi pin lấy quà”.

                    #### 3. Tham gia chương trình đổi rác lấy quà

                    - Một số tổ chức triển khai chương trình “Đổi pin cũ lấy cây xanh”, “Đổi pin lấy điểm thưởng” để khuyến khích người dân xử lý đúng cách.

                    ### IV. Những điều tuyệt đối tránh

                    - **Không đốt pin**
                      - Đốt pin sẽ sinh ra khí độc, tạo áp suất bên trong dẫn đến nổ, gây cháy lan.

                    - **Không chôn lấp tùy tiện**
                      - Các chất độc ngấm vào đất gây ô nhiễm nguồn nước và thực phẩm.

                    - **Không vứt vào nước**
                      - Dù là ao, hồ hay cống rãnh, pin sẽ giải phóng chất độc khiến nước bị ô nhiễm.

                    ### V. Vai trò của doanh nghiệp và cộng đồng

                    - **Doanh nghiệp sản xuất điện tử** cần có chính sách thu hồi pin và thiết bị cũ.
                    - **Chính quyền địa phương** cần lắp đặt thùng thu gom pin tại khu dân cư, trường học, cơ quan.
                    - **Người dân** cần chủ động phân loại và xử lý đúng nơi quy định.

                    ### VI. Một số mô hình tại Việt Nam

                    - **TP.HCM:** Đã có hơn 500 thùng thu gom pin đặt tại siêu thị, trường học. Mỗi tháng thu về hàng tấn pin cũ để vận chuyển về nhà máy xử lý chuyên biệt.
                    - **Hà Nội:** Dự án “Pin sạch – Sống xanh” với gần 200 điểm thu gom ở các quận nội thành.
                    - **Đà Nẵng:** Kết hợp giữa ngành môi trường và học sinh trong chiến dịch “Một viên pin – Một cây xanh”.

                    ### VII. Cách làm thùng đựng pin tại nhà

                    - Dùng chai nước lọc rỗng, đục lỗ thoáng.
                    - Ghi chú rõ ràng trên vỏ chai.
                    - Khi đầy, mang đến điểm thu gom.

                    ### VIII. Định kỳ xử lý

                    - Nên kiểm tra 1 lần mỗi tháng để gom pin cũ.
                    - Tốt nhất không lưu trữ pin hỏng quá lâu trong nhà.

                    ### IX. Gợi ý thay thế pin dùng một lần

                    - Dùng **pin sạc** thay cho pin AA/AAA thông thường, có thể dùng hàng trăm lần.
                    - Chọn thiết bị điện tử có pin tích hợp và dễ thay thế, tránh dùng sản phẩm không rõ nguồn gốc.

                    ### Kết luận

                    Xử lý pin cũ an toàn không chỉ là việc làm cá nhân mà còn là trách nhiệm tập thể để bảo vệ môi trường và tương lai thế hệ sau. Hành động nhỏ như thu gom pin đúng cách, mang đến điểm thu nhận hoặc hướng dẫn người thân cùng làm, chính là sự đóng góp bền vững cho cộng đồng. Hãy bắt đầu từ hôm nay – đừng để viên pin nhỏ gây ra hiểm họa lớn!",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fpincu.png?alt=media",
                    VideoUrl = "https://www.youtube.com/watch?v=wU3_vtJjd-Q",
                    WasteId = "66666666-6666-6666-6666-666666666666"
                },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tái chế điện thoại hỏng",
                    Content = @"Trong thời đại công nghệ phát triển nhanh chóng, điện thoại di động trở thành vật dụng không thể thiếu của mỗi người. Tuy nhiên, với tốc độ thay mới liên tục, số lượng điện thoại cũ, hỏng không ngừng tăng lên, tạo ra một lượng lớn rác thải điện tử (e-waste). Nếu không được xử lý đúng cách, điện thoại hỏng có thể gây ô nhiễm môi trường nghiêm trọng và ảnh hưởng xấu đến sức khỏe con người. Việc tái chế điện thoại cũ là một bước đi quan trọng trong hành trình bảo vệ môi trường và phát triển bền vững.

                    ### I. Tại sao cần tái chế điện thoại hỏng?

                    1. **Chứa các linh kiện độc hại**
                       - Trong điện thoại có chứa các kim loại nặng như chì, thủy ngân, cadmium, arsenic... dễ rò rỉ ra môi trường khi thiết bị bị phá vỡ, chôn lấp hoặc đốt cháy.

                    2. **Ô nhiễm môi trường đất và nước**
                       - Khi vứt bừa bãi, các chất độc từ điện thoại thấm vào đất, mạch nước ngầm và ảnh hưởng lâu dài đến cây trồng, động vật và con người.

                    3. **Phát sinh khí độc**
                       - Đốt thiết bị điện tử phát sinh khí độc như dioxin, furan – những chất cực kỳ nguy hiểm với sức khỏe, có thể gây ung thư.

                    4. **Lãng phí tài nguyên quý**
                       - Trong điện thoại có chứa vàng, bạc, đồng và các kim loại hiếm như palladium. Tái chế giúp tiết kiệm nguồn tài nguyên quý hiếm và giảm nhu cầu khai khoáng.

                    ### II. Điện thoại hỏng được tái chế như thế nào?

                    1. **Tháo rời linh kiện**
                       - Các trung tâm tái chế tách từng bộ phận: màn hình, pin, mạch điện, vỏ nhựa/kim loại…

                    2. **Phân loại vật liệu**
                       - Nhựa, kim loại, thủy tinh được phân loại riêng để xử lý tái chế theo công nghệ tương ứng.

                    3. **Chiết xuất kim loại quý**
                       - Từ bo mạch điện tử, các nhà máy có thể chiết xuất vàng, bạc, đồng để tái sử dụng.

                    4. **Xử lý pin riêng biệt**
                       - Pin lithium được phân loại và đưa đến nhà máy chuyên xử lý rác nguy hại.

                    ### III. Hướng dẫn xử lý điện thoại hỏng đúng cách

                    #### 1. Không vứt vào thùng rác thông thường

                    - Dù điện thoại hỏng, tuyệt đối không vứt cùng rác sinh hoạt vì sẽ ảnh hưởng đến quá trình xử lý rác và dễ gây ô nhiễm.

                    #### 2. Xóa dữ liệu cá nhân

                    - Trước khi đem đi tái chế hoặc cho tặng, hãy reset (khôi phục cài đặt gốc) và tháo SIM, thẻ nhớ để bảo vệ thông tin cá nhân.

                    #### 3. Giao cho nơi tiếp nhận uy tín

                    Bạn có thể mang điện thoại cũ/hỏng đến:
                    - Trung tâm bảo hành, nhà phân phối điện thoại.
                    - Các điểm thu gom thiết bị điện tử do thành phố tổ chức.
                    - Chương trình tái chế điện tử từ các thương hiệu lớn như Apple, Samsung, FPT, Thế Giới Di Động, Viettel Store…
                    - Một số chiến dịch môi trường như “Đổi rác lấy quà”, “Green E-waste Day”,...

                    #### 4. Tặng lại nếu còn giá trị sử dụng

                    - Nếu điện thoại chỉ hư nhẹ, có thể sửa chữa rồi tặng lại cho người có hoàn cảnh khó khăn, hoặc bán lại cho cửa hàng thu mua.

                    ### IV. Các hoạt động tái chế điện thoại tại Việt Nam

                    - **Apple Trade-in**: Người dùng có thể đổi điện thoại cũ lấy giảm giá khi mua máy mới.
                    - **Samsung Collection Program**: Các trung tâm bảo hành Samsung tiếp nhận điện thoại cũ để xử lý đúng cách.
                    - **GreenHub Vietnam**: Tổ chức môi trường thực hiện chiến dịch thu gom rác điện tử tại trường học, công ty.
                    - **TP.HCM và Hà Nội**: Tổ chức nhiều đợt “Tuần lễ rác điện tử” để người dân mang điện thoại, laptop cũ đến xử lý miễn phí.

                    ### V. Lưu ý quan trọng

                    - Không tự ý tháo rời điện thoại nếu bạn không có kiến thức chuyên môn, vì pin hoặc mạch điện có thể gây cháy nổ.
                    - Không bán điện thoại hỏng cho cơ sở không rõ nguồn gốc vì có thể gây rò rỉ thông tin cá nhân hoặc xử lý sai quy chuẩn.
                    - Với các dòng điện thoại cũ giá trị, nên xóa toàn bộ tài khoản iCloud, Google trước khi giao nộp.

                    ### VI. Giá trị sau tái chế

                    - Theo nghiên cứu, 1 triệu điện thoại có thể tái chế được:
                      - 34 kg vàng
                      - 350 kg bạc
                      - 15 tấn đồng
                      - Hàng trăm kg nhôm, thủy tinh và nhựa

                    Tái chế điện thoại không chỉ tiết kiệm hàng triệu USD chi phí khai thác mà còn giúp giảm phát thải khí nhà kính và giảm ô nhiễm đất, nước.

                    ### VII. Hành động nhỏ – Tác động lớn

                    - **Gia đình**: Lập một hộp nhỏ đựng thiết bị cũ để khi đủ số lượng sẽ mang đi tái chế.
                    - **Trường học**: Phát động phong trào “Không vứt rác điện tử” và tổ chức ngày thu gom định kỳ.
                    - **Doanh nghiệp**: Tổ chức thu gom trong công ty, hỗ trợ nhân viên xử lý rác công nghệ đúng cách.

                    ### Kết luận

                    Điện thoại hỏng không phải là rác vô dụng. Nếu được tái chế đúng cách, chúng trở thành nguồn tài nguyên quý giá cho nền kinh tế tuần hoàn. Hãy dừng việc vứt bừa thiết bị điện tử, bắt đầu phân loại và đưa đến nơi xử lý chuyên nghiệp. Việc làm nhỏ của bạn hôm nay sẽ tạo nên một tương lai xanh – sạch – bền vững cho mai sau.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fdienthoaihong.jpg?alt=media",
                    VideoUrl = "https://www.youtube.com/watch?v=vpj9_Zj4LAQ",
                    WasteId = "77777777-7777-7777-7777-777777777777"
                },
                new RecycleGuides
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tái sử dụng chai thủy tinh",
                    Content = @"Chai thủy tinh là một trong những loại bao bì được sử dụng phổ biến trong đời sống hàng ngày, từ đựng nước mắm, rượu, nước ép, mật ong cho đến mỹ phẩm và dược phẩm. Nhờ đặc tính bền, không phản ứng hóa học và dễ làm sạch, chai thủy tinh có tiềm năng lớn trong việc tái sử dụng và tái chế. Tuy nhiên, trên thực tế, rất nhiều chai thủy tinh vẫn bị vứt bỏ sau một lần sử dụng, tạo gánh nặng cho môi trường và gây lãng phí tài nguyên. Hướng dẫn sau sẽ giúp bạn khai thác tối đa giá trị của chai thủy tinh cũ.

                    ### I. Vì sao nên tái sử dụng chai thủy tinh?

                    1. **Bền và an toàn**
                       - Thủy tinh không bị ăn mòn bởi hóa chất, không thôi nhiễm, không hấp thụ mùi và màu thực phẩm. Khi được rửa sạch, chai thủy tinh có thể dùng lại nhiều lần mà không ảnh hưởng đến chất lượng.

                    2. **Tiết kiệm chi phí**
                       - Tái sử dụng chai giúp giảm chi phí mua bao bì mới. Đặc biệt với quán ăn, hộ kinh doanh nhỏ, việc tái sử dụng có thể tiết kiệm đáng kể.

                    3. **Giảm rác thải môi trường**
                       - Chai thủy tinh rất lâu phân hủy nếu bị chôn lấp. Tái sử dụng sẽ giúp giảm lượng rác vô cơ tồn đọng trong thiên nhiên.

                    4. **Tiết kiệm năng lượng sản xuất**
                       - Sản xuất một chai thủy tinh mới cần rất nhiều nhiệt và nguyên liệu. Tái sử dụng giúp giảm năng lượng và tài nguyên đầu vào.

                    ### II. Những cách tái sử dụng chai thủy tinh

                    #### 1. Dùng để đựng thực phẩm và gia vị

                    - Đựng dầu ăn, nước mắm, xì dầu, giấm...
                    - Đựng muối, đường, hạt tiêu, bột canh...
                    - Đựng hạt giống, đậu, gạo mini để trồng cây trong nhà.

                    #### 2. Làm bình nước uống cá nhân

                    - Rửa sạch chai thủy tinh, trang trí bằng băng keo màu, dây thừng để tạo phong cách riêng.
                    - Có thể dùng để đựng nước lọc, nước detox trong tủ lạnh hoặc mang theo khi đi làm.

                    #### 3. Chai cắm hoa, trang trí bàn

                    - Dùng chai thủy tinh cao cổ làm bình hoa mini cho phòng khách, bàn học, bệ cửa sổ.
                    - Có thể sơn màu hoặc dùng dây bố, ruy băng quấn quanh để tăng tính thẩm mỹ.

                    #### 4. Đèn trang trí handmade

                    - Đặt đèn LED nhỏ vào bên trong chai thủy tinh để làm đèn ngủ, đèn không gian quán café, nhà hàng.
                    - Tạo hiệu ứng lung linh khi đặt ở góc làm việc hoặc bệ cửa sổ.

                    #### 5. Làm nến thơm tại nhà

                    - Đổ sáp nến vào chai thủy tinh cắt ngắn, thêm tinh dầu và bấc để tạo nến handmade.
                    - Thích hợp làm quà tặng, hoặc sử dụng cá nhân.

                    #### 6. Đựng mỹ phẩm tự chế

                    - Dùng để đựng toner, dầu dừa, dầu oliu, nước hoa hồng tự làm tại nhà.
                    - Có thể gắn nhãn và ngày pha chế để kiểm soát hạn dùng.

                    ### III. Một số lưu ý khi tái sử dụng

                    - **Làm sạch hoàn toàn:** Trước khi sử dụng lại, phải rửa sạch bằng nước nóng và xà phòng, có thể tráng thêm bằng giấm hoặc nước muối loãng.
                    - **Loại bỏ mùi cũ:** Ngâm chai trong nước ấm với vài lát chanh hoặc baking soda để khử mùi.
                    - **Không tái sử dụng chai vỡ, nứt:** Tránh nguy cơ cắt trúng tay hoặc vỡ trong quá trình sử dụng.

                    ### IV. Các ý tưởng sáng tạo khác

                    - **Chai đựng tiền tiết kiệm:** Cắt khe trên nắp chai để làm hũ tiết kiệm trong suốt.
                    - **Làm ống đựng bút:** Cắt ngắn chai để bàn học thêm gọn gàng.
                    - **Trồng cây thủy canh:** Cho đất và cây nhỏ vào chai để trang trí bàn hoặc treo lên tường.
                    - **Chai chứa thư tay:** Tạo quà tặng bất ngờ với lời chúc bên trong chai như trong các bộ phim lãng mạn.

                    ### V. Tái chế chai thủy tinh đúng cách

                    Khi chai không thể dùng lại (bể, vỡ), hãy:
                    - Gom riêng và không trộn với rác hữu cơ.
                    - Bọc kỹ để tránh làm bị thương người thu gom.
                    - Mang đến điểm thu gom thủy tinh tại:
                      - Các trung tâm phân loại rác tái chế.
                      - Một số chợ, siêu thị lớn có lắp đặt thùng phân loại.
                      - Chương trình đổi rác tái chế lấy điểm.

                    ### VI. Thực trạng và hành động tại Việt Nam

                    - Hiện nay, tỷ lệ tái sử dụng chai thủy tinh còn thấp do thiếu điểm thu gom chuyên biệt.
                    - Một số doanh nghiệp như Habeco, Sapporo, và các hãng nước ngọt đã triển khai chương trình thu hồi vỏ chai để tái sử dụng.
                    - Một số quán cà phê, homestay thân thiện môi trường bắt đầu chuyển sang dùng chai thủy tinh refill (nạp lại) thay vì chai nhựa một lần.

                    ### VII. Tái sử dụng tại doanh nghiệp, cộng đồng

                    - **Quán cà phê:** Tái sử dụng chai để đựng cold brew, nước detox.
                    - **Trường học:** Hướng dẫn học sinh làm đồ thủ công tái chế từ chai thủy tinh.
                    - **Hộ gia đình:** Dạy trẻ em phân loại và tái sử dụng chai, hình thành thói quen xanh từ sớm.

                    ### Kết luận

                    Tái sử dụng chai thủy tinh không chỉ giúp giảm thiểu rác thải và bảo vệ môi trường mà còn tạo ra nhiều cơ hội sáng tạo và tiết kiệm trong cuộc sống hàng ngày. Từ những vật dụng tưởng chừng đơn giản và đã qua sử dụng, bạn hoàn toàn có thể biến chúng thành đồ dùng hữu ích, vật trang trí độc đáo hoặc sản phẩm tái chế thân thiện. Hãy bắt đầu từ một chai, một hành động nhỏ mỗi ngày – vì một Việt Nam xanh hơn, sạch hơn và bền vững hơn.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fchaithuytinh.jpg?alt=media",
                    VideoUrl = "https://www.youtube.com/watch?v=AEZPZ-ioQbs",
                    WasteId = "88888888-8888-8888-8888-888888888888"
                }

            );

            builder.Entity<BlogTypes>().HasData(
                new BlogTypes
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Tuyên Truyền",
                    Description = "Bài viết tuyên truyền, hướng dẫn phân loại và xử lý rác thải"
                },
                new BlogTypes
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "Tin tức",
                    Description = "Tin tức và sự kiện liên quan đến bảo vệ môi trường và phân loại rác"
                },
                new BlogTypes
                {
                    Id = "33333333-3333-3333-3333-333333333333",
                    Name = "Chiến dịch",
                    Description = "Thông tin về các chiến dịch thu gom, phân loại rác tại cộng đồng"
                },
                new BlogTypes
                {
                    Id = "44444444-4444-4444-4444-444444444444",
                    Name = "Hướng dẫn",
                    Description = "Các bài viết hướng dẫn cách phân loại rác đúng cách tại nguồn"
                },
                new BlogTypes
                {
                    Id = "55555555-5555-5555-5555-555555555555",
                    Name = "Hỏi đáp",
                    Description = "Giải đáp thắc mắc của người dân về phân loại, thu gom và xử lý rác"
                }
            );

            builder.Entity<Blogs>().HasData(
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Cách phân loại rác nhựa đúng cách",
                    Content = @"Rác thải nhựa là một trong những nguyên nhân chính gây ô nhiễm môi trường hiện nay. Việc phân loại rác nhựa đúng cách không chỉ giúp tiết kiệm tài nguyên mà còn góp phần bảo vệ hệ sinh thái và giảm tải cho các bãi chôn lấp rác. Bài viết này sẽ hướng dẫn bạn cách phân loại rác nhựa một cách chi tiết và hiệu quả tại nhà.

                    Trước tiên, chúng ta cần hiểu rác nhựa là gì. Rác nhựa bao gồm các loại sản phẩm được làm từ vật liệu nhựa như chai lọ, hộp, túi nylon, bao bì thực phẩm, dụng cụ sinh hoạt... Có hai nhóm chính: nhựa có thể tái chế và nhựa không thể tái chế. Nhựa có thể tái chế là những loại nhựa cứng như chai PET, hộp PP, can HDPE… trong khi đó nhựa không tái chế thường là nylon mỏng, xốp PE, ống hút, hoặc các loại nhựa bị nhiễm bẩn, lẫn tạp chất.

                    Để phân loại hiệu quả, bước đầu tiên là **làm sạch rác nhựa**. Các vật dụng như chai nước ngọt, hộp đựng thực phẩm cần được rửa sạch và để khô trước khi đem đi phân loại. Nếu nhựa bị dính dầu mỡ hoặc thức ăn, khả năng tái chế sẽ giảm đi rất nhiều, thậm chí không thể tái chế được.

                    Bước tiếp theo là **tách riêng các loại nhựa theo ký hiệu tái chế**. Hầu hết các sản phẩm nhựa đều có ký hiệu hình tam giác với số từ 1 đến 7, đại diện cho loại nhựa. Ví dụ:

                    - **PET (1)**: Chai nước, chai nước ngọt, chai dầu ăn. Dễ tái chế.
                    - **HDPE (2)**: Can nhựa, chai sữa. Tái chế tốt.
                    - **PVC (3)**: Ống nhựa, màng bọc. Khó tái chế, không nên dùng lại.
                    - **LDPE (4)**: Túi nylon, màng nhựa. Khó tái chế.
                    - **PP (5)**: Hộp đựng thức ăn, ống hút. Có thể tái chế.
                    - **PS (6)**: Hộp xốp, muỗng nhựa. Khó tái chế.
                    - **Other (7)**: Các loại nhựa phức hợp. Khó tái chế nhất.

                    Sau khi phân loại, bạn nên **đựng nhựa tái chế vào các túi riêng biệt**, ghi chú rõ loại nhựa nếu có thể. Nếu địa phương bạn có chương trình thu gom rác tái chế, hãy tuân theo lịch trình và hướng dẫn từ chính quyền địa phương. Nếu không có, bạn có thể liên hệ với các đơn vị thu gom rác tái chế hoặc mang đến các điểm thu gom tự nguyện.

                    Ngoài việc phân loại, hãy thực hành **giảm thiểu sử dụng nhựa một lần**. Sử dụng chai thủy tinh, hộp inox, túi vải thay vì túi nylon và đồ nhựa dùng một lần. Việc tái sử dụng sẽ giúp bạn giảm lượng rác thải phát sinh đáng kể.

                    **Lưu ý quan trọng:**

                    - Không bỏ rác nhựa vào chung với rác hữu cơ hoặc rác y tế.
                    - Tránh nhầm lẫn giữa các loại nhựa – nếu không chắc chắn có thể hỏi chuyên gia môi trường hoặc tổ dân phố.
                    - Không đốt nhựa – việc này thải ra các khí độc hại, ảnh hưởng nghiêm trọng đến sức khỏe.

                    Theo thống kê từ tổ chức Greenpeace, mỗi năm Việt Nam thải ra hơn 1,8 triệu tấn rác thải nhựa, trong đó chỉ có khoảng 27% được tái chế. Phần còn lại chôn lấp hoặc thải ra môi trường, gây ô nhiễm nguồn nước, đất, không khí và đe dọa nghiêm trọng đến các loài động vật hoang dã. Vì vậy, **việc phân loại rác nhựa không còn là trách nhiệm của riêng ai mà là nghĩa vụ chung của toàn xã hội**.

                    Hãy bắt đầu từ những hành động nhỏ: không dùng ống hút nhựa, mang theo túi vải khi đi chợ, từ chối nhận túi nylon khi không cần thiết, và hướng dẫn người thân, bạn bè cùng phân loại rác đúng cách.

                    **Tổng kết:** Phân loại rác nhựa tuy đơn giản nhưng đòi hỏi sự kiên trì và ý thức bảo vệ môi trường. Hành động này tuy nhỏ nhưng sẽ tạo ra hiệu ứng lớn nếu mỗi người dân cùng chung tay thực hiện. Bảo vệ môi trường bắt đầu từ chính ngôi nhà của bạn, từ chiếc chai nước bạn sử dụng, từ túi nylon bạn từ chối. Hãy trở thành một phần của sự thay đổi tích cực cho tương lai xanh hơn.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fblogphanloairac.jpg?alt=media",
                    AuthorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogTypeId = "11111111-1111-1111-1111-111111111111",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Sự kiện thu gom rác tại thành phố HCM",
                    Content = @"Trong những năm gần đây, vấn đề ô nhiễm môi trường và quản lý chất thải rắn ngày càng trở nên nghiêm trọng tại các đô thị lớn, trong đó có thành phố X. Trước thực trạng này, vào ngày 5 tháng 6 – nhân ngày Môi trường Thế giới – thành phố X đã tổ chức một sự kiện quy mô lớn mang tên “Cùng nhau làm sạch thành phố”, thu hút sự tham gia của hơn 5.000 người dân, học sinh, sinh viên, cán bộ công chức, các tổ chức xã hội và doanh nghiệp địa phương.
                    Mục tiêu chính của sự kiện là nâng cao nhận thức cộng đồng về tầm quan trọng của việc thu gom, phân loại và xử lý rác thải đúng cách; đồng thời khuyến khích người dân cùng chung tay xây dựng một môi trường sống xanh – sạch – đẹp.
                    Sự kiện bắt đầu từ 7 giờ sáng tại Quảng trường Trung tâm với phần phát biểu khai mạc của đại diện Ủy ban Nhân dân thành phố. Trong bài phát biểu, lãnh đạo thành phố nhấn mạnh: “Bảo vệ môi trường không phải là công việc của riêng chính quyền hay các cơ quan chức năng, mà là trách nhiệm chung của mỗi người dân. Một hành động nhỏ như nhặt rác hay phân loại rác tại nguồn cũng góp phần làm thay đổi diện mạo thành phố.”
                    Ngay sau lễ khai mạc là phần diễu hành truyền thông với khẩu hiệu “Thành phố không rác”, “Hành động xanh – Tương lai xanh” do các em học sinh và đoàn viên thanh niên cầm biển, mặc áo đồng phục đi qua các tuyến đường chính. Hành động này nhằm kêu gọi mọi người dân quan tâm hơn đến vấn đề xử lý chất thải.
                    Bắt đầu từ 8 giờ, các đội hình được chia về các phường, khu phố, công viên, bờ kênh… để tiến hành thu gom rác thải. Những địa điểm vốn bị xem là “điểm đen” về rác như khu chợ cũ, bến xe nội đô, hay một số bãi đất trống ven đô đã được làm sạch đáng kể chỉ trong buổi sáng. Theo thống kê từ Ban Tổ chức, tổng cộng đã có hơn **15 tấn rác thải** được thu gom trong ngày hôm đó, trong đó có đến 5 tấn là rác có thể tái chế như chai nhựa, giấy, kim loại.
                    Bên cạnh hoạt động thu gom, các gian hàng triển lãm về tái chế rác thải, khu vực hướng dẫn phân loại rác tại nguồn cũng thu hút đông đảo người dân đến tham quan và học hỏi. Các em nhỏ được hướng dẫn cách tái sử dụng hộp sữa để làm đồ chơi, người lớn được tặng túi vải, bình nước thủy tinh thay cho túi nylon và chai nhựa.
                    Nhiều doanh nghiệp cũng tham gia đồng hành bằng cách tài trợ các vật dụng phục vụ sự kiện: găng tay, bao rác, dụng cụ gắp rác, cũng như huy động nhân sự tham gia thu gom rác theo từng nhóm. Một số công ty thậm chí còn cam kết triển khai chương trình “Nói không với nhựa dùng một lần” tại văn phòng và cơ sở sản xuất của mình.
                    Ngoài ra, sự kiện còn ghi nhận sự tham gia tích cực của người dân địa phương. Một cụ bà 72 tuổi ở phường B cho biết: “Tôi rất vui vì thấy cả khu phố cùng nhau ra đường dọn dẹp. Không khí vui vẻ, đoàn kết, ai cũng hăng say làm việc. Tôi mong mỗi tuần đều có hoạt động thế này.”
                    Đặc biệt, buổi chiều diễn ra tọa đàm “Giải pháp bền vững cho quản lý chất thải rắn đô thị” với sự tham gia của các chuyên gia, nhà khoa học, đại diện các tổ chức môi trường và chính quyền địa phương. Các giải pháp như mở rộng mô hình phân loại rác tại nguồn, triển khai trạm tái chế thông minh, áp dụng công nghệ vào giám sát và thu gom rác đã được đưa ra bàn luận sôi nổi.
                    Sau sự kiện, chính quyền thành phố cũng tuyên bố sẽ phát động chương trình “Thành phố X không rác thải” kéo dài 6 tháng, với các hoạt động định kỳ như: tuần lễ làm sạch khu phố, cuộc thi tái chế dành cho học sinh – sinh viên, chiến dịch đổi rác lấy quà tặng, và nhân rộng các mô hình cộng đồng tự quản về môi trường.
                    Thành công của sự kiện “Cùng nhau làm sạch thành phố” là minh chứng rõ ràng cho sức mạnh của sự đoàn kết cộng đồng và ý thức bảo vệ môi trường. Qua đó, không chỉ làm sạch môi trường sống, mà còn lan tỏa lối sống xanh – sạch – có trách nhiệm đến từng người dân.

                    **Kết luận:** Việc tổ chức các sự kiện thu gom rác không chỉ mang lại lợi ích ngắn hạn là làm sạch thành phố, mà còn xây dựng thói quen, nhận thức và tinh thần cộng đồng bền vững. Với sự vào cuộc đồng bộ của chính quyền, người dân và doanh nghiệp, thành phố X hoàn toàn có thể trở thành hình mẫu về quản lý chất thải đô thị và phát triển bền vững trong tương lai.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fsukienthugomrac.jpg?alt=media",
                    AuthorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    BlogTypeId = "22222222-2222-2222-2222-222222222222",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Hướng dẫn phân loại rác thải hữu cơ tại nhà",
                    Content = @"Rác thải hữu cơ chiếm tỉ lệ lớn trong tổng lượng rác sinh hoạt hàng ngày, nhưng lại thường bị bỏ lẫn cùng rác vô cơ khiến cho việc xử lý trở nên khó khăn, tốn kém và gây ô nhiễm môi trường. Việc phân loại rác hữu cơ tại nguồn là bước đi quan trọng giúp giảm gánh nặng cho các bãi rác, tiết kiệm chi phí xử lý và tận dụng nguồn nguyên liệu tự nhiên cho nông nghiệp.
                    Vậy rác thải hữu cơ là gì? Đây là các loại rác có nguồn gốc từ thực phẩm và sinh vật sống như: vỏ rau củ quả, cơm thừa, thịt cá, bã cà phê, lá cây, hoa héo, bánh mỳ mốc... Những loại rác này có thể phân hủy sinh học, từ đó được xử lý thành phân bón hữu cơ, khí sinh học hoặc sử dụng trong trồng trọt, chăn nuôi.
                    
                    **Các bước phân loại rác hữu cơ tại nhà:**
                    1. **Chuẩn bị dụng cụ chứa rác hữu cơ riêng biệt:** Bạn nên sử dụng thùng có nắp đậy, dễ vệ sinh. Nên để nơi thoáng mát để tránh mùi hôi.
                    2. **Tách riêng ngay từ nguồn:** Khi sơ chế thực phẩm hoặc dọn bữa ăn, cần bỏ riêng các phần vỏ, rác thức ăn thừa vào thùng hữu cơ, không trộn lẫn với túi nylon, hộp xốp hoặc các vật dụng nhựa.
                    3. **Giảm độ ẩm và khối lượng:** Có thể cắt nhỏ rác hữu cơ và để ráo nước trước khi bỏ vào thùng. Điều này giúp hạn chế ruồi nhặng, mùi hôi và thúc đẩy quá trình phân hủy nhanh hơn.
                    4. **Ủ rác hữu cơ:** Bạn có thể làm phân compost ngay tại nhà bằng cách ủ rác hữu cơ trong thùng có lỗ thoáng khí, trộn với đất, mùn cưa hoặc chế phẩm vi sinh. Sau khoảng 30-45 ngày, hỗn hợp sẽ phân hủy thành phân bón màu nâu đen, mùi dễ chịu, rất tốt cho cây trồng.

                    **Một số lưu ý quan trọng:**
                    - Không nên cho vào thùng hữu cơ các loại rác như dầu mỡ, xương lớn, vỏ hải sản, chất hóa học, vì chúng phân hủy chậm và dễ gây mùi.
                    - Nếu không có thời gian ủ, bạn có thể đựng rác hữu cơ và giao cho đơn vị thu gom riêng biệt nếu địa phương bạn triển khai chương trình phân loại tại nguồn.
                    - Rửa sạch thùng chứa thường xuyên để đảm bảo vệ sinh và tránh côn trùng.
                    Ngoài phương pháp truyền thống, hiện nay có nhiều gia đình sử dụng máy ủ rác hữu cơ mini hoặc nuôi giun quế để xử lý. Giun ăn rác hữu cơ và thải ra phân giun rất giàu dưỡng chất. Đây là một mô hình xanh, hiệu quả, phù hợp với người yêu thiên nhiên và làm vườn tại nhà.

                    **Tại sao cần phân loại rác hữu cơ?**
                    - **Giảm lượng rác thải chôn lấp:** Nếu phân loại đúng, rác hữu cơ không còn chiếm đến 50-60% rác sinh hoạt.
                    - **Tiết kiệm chi phí xử lý rác:** Rác hữu cơ được xử lý tại nguồn sẽ giảm áp lực cho công ty môi trường.
                    - **Tạo nguồn phân bón tự nhiên:** Giảm phụ thuộc vào phân hóa học, phục vụ nông nghiệp sạch.
                    - **Giảm phát thải khí nhà kính:** Rác hữu cơ bị chôn lấp sẽ sinh ra khí methane, gây hiệu ứng nhà kính mạnh gấp 25 lần CO₂.
                    Theo báo cáo của Bộ Tài nguyên và Môi trường, mỗi ngày thành phố Hồ Chí Minh thải ra gần 10.000 tấn rác, trong đó có đến 60% là rác hữu cơ. Tuy nhiên, hiện nay chỉ có khoảng 10% số này được xử lý đúng cách để tái chế thành phân compost. Phần lớn còn lại bị chôn lấp chung với rác vô cơ, gây lãng phí và ô nhiễm.

                    **Hành động bắt đầu từ mỗi gia đình:** Bạn không cần phải làm điều gì quá to lớn. Chỉ cần chuẩn bị thêm một thùng rác, thay đổi thói quen bỏ rác và kiên trì trong một thời gian ngắn, bạn sẽ thấy kết quả rõ rệt. Nếu bạn có trẻ nhỏ, hãy hướng dẫn các em cách phân loại rác như một trò chơi giáo dục về môi trường.

                    **Tổng kết:** Phân loại rác thải hữu cơ tại nhà là một hành động nhỏ nhưng có ý nghĩa lớn. Không chỉ giúp giảm thiểu lượng rác thải ra môi trường, việc này còn giúp bạn tận dụng được nguồn tài nguyên quý giá cho trồng trọt, cải thiện chất lượng đất và sức khỏe cộng đồng. Mỗi hành động dù nhỏ bé hôm nay sẽ là viên gạch đặt nền cho một tương lai xanh và bền vững.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fphanloairactainha.jpg?alt=media",
                    AuthorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogTypeId = "44444444-4444-4444-4444-444444444444",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Giải đáp thắc mắc về phân loại rác",
                    Content = @"Phân loại rác đang trở thành một thói quen cần thiết trong cuộc sống hiện đại. Tuy nhiên, không ít người dân vẫn còn mơ hồ hoặc hiểu sai về việc phân loại rác. Bài viết này sẽ tổng hợp và giải đáp các câu hỏi thường gặp nhất xoay quanh chủ đề này, giúp bạn dễ dàng áp dụng vào thực tế.
                    
                    **1. Vì sao cần phân loại rác tại nguồn?**
                    Phân loại rác tại nguồn giúp dễ dàng tái chế, xử lý đúng cách, giảm thiểu ô nhiễm môi trường và tiết kiệm chi phí. Nếu rác thải được phân loại đúng, các công đoạn xử lý phía sau sẽ hiệu quả hơn, đồng thời giảm áp lực cho bãi chôn lấp và hạn chế phát thải khí nhà kính.
                    
                    **2. Rác được chia thành những loại nào?**
                    Thông thường, rác được chia thành ba nhóm chính:
                    - **Rác hữu cơ**: Thức ăn thừa, rau củ, vỏ trái cây…
                    - **Rác tái chế**: Nhựa, giấy, kim loại, thủy tinh sạch.
                    - **Rác còn lại/khó phân hủy**: Tã lót, giấy bẩn, gốm sứ vỡ, thuốc…
                    Tùy địa phương, có thể có thêm nhóm rác nguy hại (pin, bóng đèn, hóa chất) được phân riêng.
                    
                    **3. Làm sao để biết một loại rác có thể tái chế hay không?**
                    Thông thường, rác tái chế là các vật liệu còn sạch, khô và không bị dính tạp chất. Ví dụ:
                    - Chai nhựa sạch, giấy in không bị ướt, hộp carton chưa bị dầu mỡ → có thể tái chế.
                    - Giấy ăn, hộp xốp bẩn, chai dính thức ăn → không thể tái chế.
                    
                    **4. Có cần rửa sạch rác trước khi phân loại không?**
                    Câu trả lời là có. Đặc biệt với rác tái chế như hộp sữa, chai nhựa, lọ thủy tinh, bạn nên rửa sạch và để khô trước khi phân loại. Điều này giúp đảm bảo rác không bốc mùi, không lây nhiễm sang rác khác và thuận tiện cho khâu tái chế.
                    
                    **5. Túi nylon thuộc loại rác nào? Có tái chế được không?**
                    Túi nylon là một dạng rác khó phân hủy và không được khuyến khích sử dụng. Tuy nhiên, một số loại túi nylon dày, sạch có thể tái chế được. Các loại túi mỏng, nhiều màu, dính bẩn thường không thể tái chế và nên cho vào rác không tái chế.
                    
                    **6. Tôi ở khu vực chưa triển khai phân loại rác, tôi có nên làm không?**
                    Câu trả lời là: nên. Việc phân loại rác tại nhà luôn là hành động tích cực, ngay cả khi hệ thống thu gom chưa có quy định cụ thể. Bạn có thể tự ủ phân từ rác hữu cơ, gom riêng rác tái chế để bán ve chai, hoặc liên hệ các nhóm thu gom rác tái chế tự nguyện.
                    
                    **7. Phân loại rác có tốn nhiều thời gian không?**
                    Lúc đầu, bạn có thể cảm thấy lúng túng, nhưng chỉ sau vài ngày, việc phân loại sẽ trở thành thói quen. Việc này không tốn nhiều thời gian, đôi khi còn giúp bạn kiểm soát được lượng rác phát sinh và điều chỉnh hành vi tiêu dùng.
                    
                    **8. Có app hay công cụ nào hỗ trợ phân loại rác không?**
                    Hiện nay đã có nhiều ứng dụng hỗ trợ phân loại rác như: Rác Thải 101, GreenPoints, iRecycling,... Các ứng dụng này giúp bạn tra cứu nhanh vật phẩm mình đang cầm thuộc loại rác nào và cách xử lý đúng cách.
                    
                    **9. Phân loại sai có sao không?**
                    Nếu bạn phân loại sai, rác sẽ bị lẫn tạp chất, gây khó khăn trong việc tái chế, thậm chí toàn bộ lô rác đó có thể bị từ chối và xử lý như rác chôn lấp. Vì vậy, nếu không chắc chắn, bạn nên kiểm tra hoặc đặt vào rác không tái chế.
                    
                    **10. Tôi muốn hướng dẫn người thân, hàng xóm cùng phân loại rác, nên bắt đầu từ đâu?**
                    Bạn có thể:
                    - In bảng hướng dẫn và treo ở khu bếp hoặc khu đổ rác.
                    - Mở buổi chia sẻ nhỏ tại khu phố, tổ dân cư.
                    - Tổ chức cuộc thi tái chế vui nhộn.
                    - Dẫn chứng từ lợi ích thiết thực như giảm mùi hôi, nhà sạch hơn, có thể bán rác tái chế...
                    
                    **Kết luận:** Phân loại rác là việc ai cũng có thể làm được. Điều quan trọng không phải là bạn biết bao nhiêu, mà là bạn bắt đầu làm từ đâu. Hãy bắt đầu từ chính ngôi nhà của bạn, hướng dẫn người thân, chia sẻ kiến thức và lan tỏa thói quen tốt này ra cộng đồng. Một xã hội văn minh bắt đầu từ những hành động nhỏ như bỏ rác đúng chỗ, phân loại đúng loại.",
                    ImageUrl = null,
                    AuthorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    BlogTypeId = "55555555-5555-5555-5555-555555555555",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Chiến dịch thu gom rác thải tại khu phố HCM",
                    Content = @"Trong bối cảnh ô nhiễm môi trường đô thị ngày càng nghiêm trọng, các chiến dịch thu gom rác thải không chỉ là hoạt động tình nguyện mang tính ngắn hạn mà còn là một bước quan trọng trong việc nâng cao nhận thức và hình thành thói quen bảo vệ môi trường cho cộng đồng. Một trong những mô hình tiêu biểu chính là chiến dịch thu gom rác thải tại khu phố Y – nơi người dân đã và đang làm nên những điều tích cực từ những hành động nhỏ nhất.
                    Chiến dịch được khởi xướng bởi Ban quản lý khu phố phối hợp cùng Đoàn thanh niên phường và các tổ chức môi trường địa phương. Với chủ đề “Sống xanh từ con phố nhỏ”, chiến dịch diễn ra trong vòng một tuần, từ ngày 12 đến ngày 18 tháng 5, với các hoạt động chính: tổng vệ sinh đường phố, tuyên truyền phân loại rác tại nguồn, hướng dẫn tái chế rác thải sinh hoạt và tổ chức điểm thu gom rác tái chế lưu động.
                    Ngay trong ngày đầu tiên phát động, hơn 300 người dân đã tình nguyện tham gia. Đặc biệt, có cả những em nhỏ cùng cha mẹ quét rác, nhặt túi nylon, phân loại chai lọ nhựa ngay tại sân chung cư. Một bác tổ trưởng dân phố chia sẻ: “Tôi chưa từng thấy tinh thần người dân lại đồng lòng như vậy. Họ không chỉ làm vì phong trào mà thực sự hiểu được ý nghĩa của việc giữ gìn môi trường sống.”
                    Khu vực được chú trọng dọn dẹp nhất là các tuyến đường nội khu, hẻm nhỏ thường bị tồn đọng rác lâu ngày, những bãi đất trống nơi người dân thường vứt rác bừa bãi. Ban tổ chức đã trang bị găng tay, kẹp gắp rác, xe thu gom và phát túi phân loại để người dân chia rác thành ba loại: rác hữu cơ, rác tái chế và rác khó phân hủy. Trong suốt một tuần, hơn 5 tấn rác đã được thu gom, trong đó có tới 40% là rác tái chế.
                    Ngoài việc làm sạch môi trường, chiến dịch còn lồng ghép các hoạt động tuyên truyền qua hình thức dễ tiếp cận như: phát tờ rơi, tổ chức trò chơi cho thiếu nhi, chiếu phim ngắn về môi trường, thi thiết kế vật dụng từ rác tái chế. Một điểm nổi bật là sự kiện “Đổi rác lấy cây” thu hút hơn 200 lượt người tham gia, mỗi người chỉ cần mang 2kg rác tái chế sẽ được đổi lấy một chậu cây xanh hoặc túi vải thân thiện.
                    Một thành viên của đội thanh niên chia sẻ: “Trước đây mình chưa từng quan tâm đến việc phân loại rác, nhưng khi được hướng dẫn rõ ràng và thấy hiệu quả thực tế thì mình thấy đây là việc hoàn toàn khả thi. Sau chiến dịch, mình vẫn duy trì thói quen này ở nhà.”
                    Không chỉ dừng lại ở thu gom, chiến dịch còn xây dựng mô hình “Góc xanh cộng đồng” – nơi đặt các thùng rác phân loại có hướng dẫn chi tiết kèm theo bảng tuyên truyền. Một nhóm tình nguyện viên sẽ phụ trách kiểm tra, hướng dẫn người dân bỏ rác đúng loại vào các khung giờ cao điểm.
                    Quan trọng hơn cả, chiến dịch đã góp phần thay đổi nhận thức của người dân khu phố Y. Trước đây, không ít người vẫn còn thờ ơ với chuyện rác thải, đổ rác không đúng giờ, không phân loại, thậm chí đốt rác gây ô nhiễm. Sau chiến dịch, số lượng vi phạm giảm rõ rệt, người dân chủ động hỏi thông tin về tái chế, nhắc nhở nhau giữ gìn vệ sinh chung.

                    Theo ghi nhận của Ban quản lý khu phố, sau một tháng kể từ khi kết thúc chiến dịch:
                    - Tỷ lệ người dân thực hiện phân loại rác tại nguồn tăng từ 10% lên 65%.
                    - Số vụ vứt rác sai quy định giảm 80%.
                    - Khuôn viên sinh hoạt cộng đồng sạch sẽ, được trang trí bởi cây xanh tái sử dụng từ chai nhựa.

                    **Bài học rút ra từ chiến dịch:**
                    - Muốn người dân tham gia, cần phải tạo điều kiện cụ thể (cung cấp vật dụng, hướng dẫn tận nơi).
                    - Tạo ra các hoạt động cộng đồng có ý nghĩa thiết thực, niềm vui (thi đua, đổi quà, trò chơi).
                    - Truyền thông gần gũi, dùng chính người dân để lan tỏa thói quen.

                    **Kết luận:** Chiến dịch thu gom rác thải tại khu phố Y là một mô hình mẫu mực về sự phối hợp giữa chính quyền, người dân và các tổ chức xã hội. Điều quan trọng không phải là quy mô chiến dịch, mà là hiệu quả thực tế và sự thay đổi lâu dài trong nhận thức, hành vi của cộng đồng. Hy vọng rằng mô hình này sẽ tiếp tục được nhân rộng và lan tỏa đến nhiều khu phố khác trên toàn quốc – nơi mỗi người dân trở thành một chiến binh bảo vệ môi trường từ chính nơi mình sinh sống.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fchiendichthugomrac.jpg?alt=media",
                    AuthorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogTypeId = "33333333-3333-3333-3333-333333333333",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tin tức mới về xử lý rác thải điện tử",
                    Content = @"Rác thải điện tử (hay còn gọi là e-waste) đang ngày càng trở thành mối đe dọa lớn đối với môi trường và sức khỏe con người. Với sự phát triển nhanh chóng của công nghệ, các thiết bị điện tử như điện thoại, máy tính, tivi, pin, thiết bị gia dụng… sau khi hỏng hóc hoặc lỗi thời bị thải bỏ với số lượng ngày càng lớn. Việt Nam mỗi năm phát sinh hàng trăm ngàn tấn rác điện tử, trong khi hệ thống xử lý còn nhiều hạn chế. Gần đây, Chính phủ và các tổ chức xã hội đã có nhiều động thái nhằm siết chặt quản lý và nâng cao hiệu quả xử lý rác điện tử.

                    **1. Những con số đáng báo động**

                    Theo báo cáo của Liên minh Viễn thông Quốc tế (ITU), Việt Nam nằm trong nhóm các quốc gia đang phát triển có mức độ phát sinh rác thải điện tử nhanh nhất châu Á. Cụ thể:
                    - Trung bình mỗi người dân Việt Nam thải khoảng 3 kg rác điện tử mỗi năm.
                    - Tổng lượng rác điện tử năm 2023 tại Việt Nam ước tính vượt quá 400.000 tấn.
                    - Chỉ khoảng 10% số rác điện tử được thu gom và xử lý theo quy trình chuẩn.

                    Phần lớn rác điện tử hiện nay bị thu gom bởi các cơ sở tư nhân nhỏ lẻ, không có giấy phép hoặc thiết bị xử lý đạt chuẩn, dẫn đến nguy cơ phát tán chất độc hại như chì, thủy ngân, cadmium… ra môi trường và vào cơ thể con người.

                    **2. Chính sách mới của Nhà nước**

                    Tháng 4/2024, Bộ Tài nguyên và Môi trường ban hành Thông tư 02/2024/TT-BTNMT quy định cụ thể về việc thu hồi, xử lý sản phẩm điện, điện tử thải bỏ. Theo đó:
                    - Các nhà sản xuất, nhập khẩu thiết bị điện tử phải có trách nhiệm thu hồi sản phẩm đã qua sử dụng từ người tiêu dùng.
                    - Đơn vị sản xuất phải công bố điểm tiếp nhận, kế hoạch thu hồi và xử lý minh bạch.
                    - Các trung tâm xử lý phải đạt tiêu chuẩn môi trường và có chứng nhận từ cơ quan có thẩm quyền.

                    Đây là một phần trong chiến lược kinh tế tuần hoàn, giảm thiểu chất thải, tăng cường tái chế và tái sử dụng tài nguyên.

                    **3. Các mô hình thu gom rác điện tử hiệu quả**

                    Nhiều thành phố lớn như Hà Nội, TP.HCM, Đà Nẵng đã triển khai các điểm thu gom rác điện tử định kỳ tại phường, trường học, siêu thị. Tiêu biểu:
                    - Chương trình “Ve chai đổi điện thoại cũ” tại các cửa hàng điện máy: Người dân có thể mang thiết bị hỏng đến đổi lấy voucher hoặc cây xanh.
                    - Sáng kiến “Rác điện tử – Đổi lấy tri thức” của một nhóm sinh viên đại học đã thu gom laptop hỏng, sửa chữa và tặng lại cho học sinh vùng sâu vùng xa.
                    - Ứng dụng “Ewaste App” cho phép người dân đặt lịch thu gom rác điện tử tận nơi với các đơn vị được cấp phép.

                    Ngoài ra, nhiều doanh nghiệp cũng đang triển khai mô hình thu hồi thiết bị cũ như một phần trách nhiệm mở rộng của nhà sản xuất (EPR), ví dụ Samsung, Dell, Apple...

                    **4. Người dân cần làm gì để xử lý rác điện tử đúng cách?**

                    - **Không vứt rác điện tử vào thùng rác sinh hoạt**: Rác điện tử chứa nhiều kim loại nặng, chất độc hại có thể rò rỉ ra môi trường.
                    - **Giao cho đơn vị có giấy phép**: Liên hệ các trung tâm xử lý được cấp phép hoặc các chiến dịch thu gom định kỳ.
                    - **Tận dụng hoặc tặng lại**: Thiết bị còn dùng được có thể sửa chữa, trao tặng cho người cần.
                    - **Xóa dữ liệu cá nhân**: Với điện thoại, máy tính… cần xóa dữ liệu trước khi chuyển giao cho người khác hoặc đơn vị xử lý.

                    **5. Vai trò của giáo dục và truyền thông**

                    Việc nâng cao nhận thức cộng đồng là yếu tố then chốt. Tại nhiều trường học, việc giảng dạy về rác điện tử và cách xử lý an toàn đã được đưa vào chương trình giáo dục môi trường. Các chiến dịch truyền thông như “Đừng để rác điện tử giết môi trường”, “Mỗi thiết bị cũ – Một hành động xanh” đã giúp thay đổi hành vi người tiêu dùng.

                    **6. Tương lai của rác điện tử – cơ hội và thách thức**

                    Trong rác điện tử chứa nhiều kim loại quý như vàng, bạc, đồng – nếu được xử lý đúng, đây có thể trở thành nguồn tài nguyên tái chế có giá trị cao. Một số doanh nghiệp đã đầu tư công nghệ tái chế hiện đại, trích xuất kim loại từ bảng mạch điện tử, mang lại hiệu quả kinh tế cao.

                    Tuy nhiên, thách thức lớn nhất vẫn là sự phân tán, thiếu đồng bộ trong thu gom và thói quen vứt bỏ tùy tiện. Việc đầu tư công nghệ xử lý tốn kém, đòi hỏi chính sách hỗ trợ tài chính, kỹ thuật và khuyến khích doanh nghiệp tư nhân tham gia.

                    **Kết luận:** Rác thải điện tử là một phần không thể tránh khỏi của xã hội hiện đại. Tuy nhiên, nếu được quản lý và xử lý đúng cách, nó không chỉ tránh được nguy cơ ô nhiễm mà còn trở thành nguồn tài nguyên tái tạo. Từ chính mỗi người dân – hãy bắt đầu bằng việc không vứt rác điện tử bừa bãi, tìm hiểu điểm thu gom gần nhất và truyền cảm hứng cho cộng đồng cùng hành động vì môi trường sống bền vững.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Ftintucveracdientu.jpg?alt=media",
                    AuthorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    BlogTypeId = "22222222-2222-2222-2222-222222222222",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Cách tái chế chai thủy tinh hiệu quả",
                    Content = @"Thủy tinh là một trong những vật liệu có thể tái chế 100% mà không bị mất chất lượng hoặc độ tinh khiết. Tuy nhiên, trên thực tế, tỷ lệ tái chế chai lọ thủy tinh vẫn còn rất thấp do thiếu kiến thức phân loại và hạ tầng thu gom phù hợp. Bài viết này sẽ hướng dẫn bạn cách tái chế chai thủy tinh hiệu quả, từ tại nhà đến điểm tái chế, và góp phần giảm gánh nặng cho môi trường.

                    **1. Tại sao cần tái chế chai thủy tinh?**

                    Thủy tinh được tạo thành từ cát, soda và vôi – những nguyên liệu tự nhiên nhưng khi khai thác và sản xuất gây tiêu tốn nhiều năng lượng. Việc tái chế thủy tinh giúp:
                    - Giảm tới 30% năng lượng so với sản xuất thủy tinh mới.
                    - Giảm phát thải CO₂ ra môi trường.
                    - Hạn chế khai thác tài nguyên thiên nhiên.
                    - Tái sử dụng nhiều lần mà không giảm chất lượng sản phẩm.

                    Ngoài ra, thủy tinh nếu bị chôn lấp sẽ tồn tại hàng ngàn năm trong lòng đất. Một mảnh thủy tinh bị vỡ có thể gây nguy hiểm cho con người, động vật và gây ô nhiễm đất, nước nếu không được xử lý đúng cách.

                    **2. Các loại chai thủy tinh thường gặp**

                    Bạn có thể dễ dàng bắt gặp nhiều loại chai lọ thủy tinh trong sinh hoạt hàng ngày như:
                    - Chai nước giải khát (nước suối, nước ngọt, bia)
                    - Lọ đựng gia vị, hũ đựng thực phẩm
                    - Chai mỹ phẩm (nước hoa, serum, kem dưỡng)
                    - Chai thuốc, bình đựng hóa chất

                    Các loại chai này có thể được tái sử dụng hoặc đưa đến cơ sở tái chế, miễn là bạn thực hiện phân loại và xử lý ban đầu đúng cách.

                    **3. Hướng dẫn tái chế chai thủy tinh tại nhà**

                    **Bước 1: Thu gom và rửa sạch**

                    Chai lọ thủy tinh sau khi sử dụng nên được rửa sạch, loại bỏ hoàn toàn chất lỏng, thực phẩm còn sót lại. Nếu chai có nhãn giấy, bạn có thể ngâm nước ấm và dùng khăn lau để bóc dễ dàng. Việc làm sạch giúp tránh mùi hôi, tránh ruồi nhặng và thuận tiện cho đơn vị thu gom.

                    **Bước 2: Phân loại theo màu sắc**

                    Trong công nghiệp tái chế, thủy tinh thường được phân loại theo màu: trong suốt (clear), nâu (amber) và xanh lá (green). Mỗi loại màu sẽ được xử lý riêng biệt để sản xuất sản phẩm có màu tương tự. Bạn nên phân chia trước nếu có thể.

                    **Bước 3: Không trộn với các vật liệu khác**

                    Chai thủy tinh thường có nắp bằng kim loại hoặc nhựa. Trước khi đưa đi tái chế, hãy tháo bỏ nắp và tách riêng. Ngoài ra, không trộn thủy tinh với gốm sứ, pha lê vì chúng có thành phần khác và không thể tái chế cùng nhau.

                    **Bước 4: Tái sử dụng tại nhà**

                    Trước khi đem đi tái chế, bạn có thể:
                    - Dùng chai thủy tinh làm bình hoa, đèn trang trí.
                    - Làm lọ cắm bút, hũ đựng gia vị hoặc nến thơm DIY.
                    - Dạy trẻ nhỏ kỹ năng thủ công từ chai lọ.

                    Việc tái sử dụng sẽ giảm được số lượng rác thải và còn tạo giá trị sáng tạo, trang trí cho không gian sống.

                    **4. Nơi tiếp nhận chai thủy tinh đã qua sử dụng**

                    - **Các điểm thu gom rác tái chế**: Một số địa phương có điểm thu gom định kỳ tại phường, tổ dân phố.
                    - **Siêu thị, cửa hàng tiện lợi**: Nhiều chuỗi lớn triển khai “Góc thu gom rác tái chế” có tiếp nhận thủy tinh.
                    - **Đơn vị thu mua phế liệu**: Họ thường thu mua với số lượng lớn – phù hợp nếu bạn gom trong thời gian dài.
                    - **Chương trình đổi chai lấy quà**: Một số tổ chức môi trường có chiến dịch đổi chai thủy tinh lấy cây xanh, túi vải…

                    **5. Lưu ý khi xử lý thủy tinh vỡ**

                    - Không nên dùng tay nhặt trực tiếp, hãy dùng chổi hoặc găng tay.
                    - Cho thủy tinh vỡ vào hộp cứng hoặc bao bì có đánh dấu rõ (ví dụ: “Thủy tinh vỡ – cẩn thận”).
                    - Không trộn vào rác sinh hoạt hoặc rác hữu cơ.

                    **6. Vai trò của cá nhân trong tái chế thủy tinh**

                    Mỗi hành động nhỏ đều mang lại tác động lớn. Nếu mỗi hộ gia đình chỉ cần tái sử dụng hoặc phân loại đúng vài chai lọ thủy tinh mỗi tháng, cả thành phố sẽ giảm được hàng chục tấn rác chôn lấp.

                    Bạn có thể:
                    - Tổ chức các buổi chia sẻ trong khu phố, lớp học để lan tỏa cách tái chế thủy tinh.
                    - Chụp ảnh “trước – sau” các món đồ tái chế từ thủy tinh để truyền cảm hứng trên mạng xã hội.
                    - Hướng dẫn trẻ em phân biệt thủy tinh, nhựa, kim loại trong các hoạt động giáo dục môi trường.

                    **Kết luận:** Tái chế chai thủy tinh là một thói quen đơn giản nhưng mang lại giá trị lớn cho môi trường. Không chỉ giúp tiết kiệm tài nguyên, giảm ô nhiễm, mà còn là cơ hội để bạn sáng tạo và góp phần hình thành lối sống xanh. Hãy bắt đầu từ hôm nay, từ chính những chai thủy tinh trong gian bếp nhà bạn.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fcachtaichechaithuytinh.jpg?alt=media",
                    AuthorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogTypeId = "44444444-4444-4444-4444-444444444444",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Phân loại rác kim loại để tái chế",
                    Content = @"Kim loại là một trong những tài nguyên có thể tái chế gần như vô hạn mà không làm mất đi chất lượng nguyên bản. Tuy nhiên, trong sinh hoạt hằng ngày, rất nhiều rác kim loại lại bị vứt lẫn vào rác sinh hoạt, không được thu gom hoặc xử lý đúng cách, gây lãng phí tài nguyên và ảnh hưởng tiêu cực đến môi trường. Việc phân loại đúng rác kim loại không chỉ giúp giảm gánh nặng rác thải mà còn góp phần tiết kiệm năng lượng và bảo vệ tài nguyên thiên nhiên.

                    **1. Vì sao cần phân loại rác kim loại?**

                    Rác kim loại có thể được nấu chảy và tái sử dụng nhiều lần trong công nghiệp sản xuất mới. Theo thống kê, việc tái chế nhôm giúp tiết kiệm tới 95% năng lượng so với việc khai thác và sản xuất nhôm mới từ quặng bô-xít. Tái chế thép cũng tiết kiệm khoảng 60-70% năng lượng và giảm lượng phát thải CO₂.

                    Tuy nhiên, khi rác kim loại bị lẫn với rác ướt, thực phẩm, hoặc các tạp chất khác thì quá trình tái chế sẽ bị ảnh hưởng nghiêm trọng. Vì vậy, việc phân loại đúng tại nguồn là bước then chốt.

                    **2. Các loại rác kim loại thường gặp trong sinh hoạt**

                    - **Nhôm**: Lon nước ngọt, vỏ bia, giấy bạc bọc thức ăn, khung cửa nhôm, vỉ nướng.
                    - **Sắt – thép**: Đinh, dây thép, dao kéo hư, vỉ sắt, lon thiếc.
                    - **Đồng**: Dây điện, lõi cáp điện cũ, các thiết bị điện tử.
                    - **Kim loại hỗn hợp**: Dụng cụ nấu ăn, ổ khóa, linh kiện điện tử, pin (lưu ý: pin là rác nguy hại, cần xử lý riêng).

                    **3. Hướng dẫn phân loại rác kim loại tại nhà**

                    **Bước 1: Tách riêng các loại kim loại**

                    Tuy nhiều loại kim loại có thể tái chế, nhưng mỗi loại có giá trị và quy trình xử lý khác nhau. Bạn có thể phân ra:
                    - Nhôm (lon bia, giấy bạc)
                    - Sắt – thép (dao kéo, vỉ sắt)
                    - Đồng (dây điện cũ)
                    Việc phân loại kỹ giúp đơn vị tái chế xử lý dễ hơn, đồng thời tăng giá trị nếu bạn đem bán phế liệu.

                    **Bước 2: Làm sạch trước khi phân loại**

                    Các lon nước ngọt, hộp kim loại đựng thực phẩm cần được rửa sạch, để khô. Nếu còn thức ăn bên trong, rác sẽ dễ bị hư hỏng, gây mùi và khiến các loại rác khác không thể tái chế cùng.

                    **Bước 3: Đựng trong thùng riêng**

                    Bạn có thể chuẩn bị riêng một thùng chứa rác kim loại, ghi rõ nhãn để cả gia đình cùng biết. Khi đầy, hãy đưa đến các điểm thu gom, bán phế liệu, hoặc chương trình đổi rác tái chế.

                    **4. Cách nhận biết kim loại có thể tái chế**

                    - **Nhôm nhẹ, không hút nam châm**: Dễ tái chế, được ưa chuộng vì tiết kiệm năng lượng.
                    - **Thép hút nam châm, cứng**: Cũng có thể tái chế, nhưng cần loại bỏ sơn hoặc gỉ sét.
                    - **Đồng có màu cam đỏ, thường trong dây điện**: Giá trị cao, nên phân loại kỹ.

                    Nếu không chắc chắn, bạn có thể dùng nam châm thử – sắt và thép hút nam châm, còn nhôm thì không.

                    **5. Những rác kim loại KHÔNG nên tái chế cùng**

                    - **Pin, ắc-quy, bóng đèn huỳnh quang**: Là rác nguy hại, chứa kim loại nặng độc hại như chì, thủy ngân.
                    - **Đồ gia dụng điện tử còn gắn mạch điện, nhựa, gỗ**: Cần tháo rời từng phần để phân loại đúng.
                    - **Kim loại dính dầu mỡ, hóa chất**: Cần xử lý kỹ hoặc không nên tái chế.

                    **6. Lợi ích của việc phân loại rác kim loại**

                    - **Giảm thiểu khai thác tài nguyên**: Tái chế 1 tấn nhôm giúp tiết kiệm 4 tấn quặng và 14 megawatt giờ điện.
                    - **Tiết kiệm chi phí**: Doanh nghiệp sản xuất được giảm chi phí nguyên liệu.
                    - **Bảo vệ môi trường**: Giảm rác tồn đọng trong bãi chôn lấp, giảm phát thải khí nhà kính.
                    - **Tăng giá trị kinh tế hộ gia đình**: Kim loại phế liệu có thể bán được với giá trị cao hơn so với rác nhựa hoặc giấy.

                    **7. Các mô hình thu gom rác kim loại hiệu quả**

                    - Các điểm thu mua phế liệu dân sinh luôn tiếp nhận rác kim loại.
                    - Một số chương trình tái chế ở đô thị tổ chức thu gom hàng tuần: đổi rác kim loại lấy cây xanh, túi vải.
                    - Trường học, doanh nghiệp có thể tổ chức ngày “Không rác kim loại” để khuyến khích nhân viên, học sinh đem rác kim loại đi xử lý tập trung.

                    **8. Vai trò của cộng đồng và cá nhân**

                    Từ việc dạy trẻ nhỏ phân biệt rác kim loại, đến việc các tổ dân phố tổ chức buổi hướng dẫn phân loại, tất cả đều góp phần tạo nên cộng đồng sống xanh. Mỗi lon bia, mỗi con dao cũ nếu được xử lý đúng cách đều có thể quay trở lại dây chuyền sản xuất dưới hình dạng mới.

                    **Kết luận:** Phân loại rác kim loại là hành động đơn giản nhưng mang lại giá trị to lớn cho môi trường và xã hội. Từ hôm nay, bạn có thể bắt đầu bằng cách đặt một chiếc hộp nhỏ trong nhà để gom kim loại riêng, dạy con em nhận biết vật liệu, hoặc chia sẻ thông tin này với hàng xóm. Một cộng đồng sạch đẹp bắt đầu từ ý thức cá nhân và hành động thiết thực hằng ngày.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fphanloaitruockhitaiche.png?alt=media",
                    AuthorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    BlogTypeId = "11111111-1111-1111-1111-111111111111",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Tầm quan trọng của việc phân loại rác thải tại nguồn",
                    Content = @"Phân loại rác thải tại nguồn là bước đầu tiên và cũng là quan trọng nhất trong chuỗi xử lý chất thải đô thị. Khi rác thải được phân loại ngay từ lúc phát sinh tại hộ gia đình, cơ quan, trường học…, việc tái chế và xử lý sẽ trở nên hiệu quả hơn, tiết kiệm chi phí và giảm tác động đến môi trường. Ngược lại, nếu rác bị trộn lẫn, tất cả sẽ trở thành rác không thể tái chế, gây áp lực lớn lên các bãi rác và hệ thống xử lý chất thải.

                    **1. Phân loại rác tại nguồn là gì?**

                    Phân loại rác tại nguồn là việc chia rác thành các nhóm riêng biệt ngay tại nơi phát sinh (thường là nhà ở, văn phòng, chợ, trường học...) thay vì dồn chung tất cả vào một thùng rác. Thông thường, rác thải được chia thành 3 nhóm chính:
                    - **Rác hữu cơ**: Gồm thức ăn thừa, rau củ quả hỏng, lá cây… dễ phân hủy, có thể ủ làm phân compost.
                    - **Rác tái chế**: Gồm nhựa, giấy, kim loại, thủy tinh… có thể thu hồi và tái sử dụng.
                    - **Rác thải còn lại/khó tái chế**: Gồm đồ dùng hư hỏng, xốp, tã lót, gốm sứ, rác y tế gia đình...

                    **2. Tại sao phải phân loại rác tại nguồn?**

                    - **Tăng hiệu quả tái chế**: Nếu rác tái chế không bị nhiễm bẩn bởi rác hữu cơ, khả năng thu hồi nguyên liệu sẽ cao hơn rất nhiều.
                    - **Giảm chi phí xử lý rác**: Việc vận chuyển, phân loại lại hoặc xử lý rác hỗn hợp tốn kém và mất nhiều công sức.
                    - **Giảm ô nhiễm môi trường**: Rác hữu cơ nếu lẫn với rác khác sẽ dễ phân hủy sinh mùi hôi, sinh ra khí methane gây hiệu ứng nhà kính.
                    - **Kéo dài tuổi thọ bãi chôn lấp**: Phân loại giúp giảm khối lượng rác phải chôn lấp, giảm diện tích bãi rác và ô nhiễm nguồn nước ngầm.
                    - **Thúc đẩy nền kinh tế tuần hoàn**: Tạo nguồn nguyên liệu đầu vào cho công nghiệp tái chế, hạn chế khai thác tài nguyên thiên nhiên.

                    **3. Hậu quả nếu không phân loại rác**

                    - Tất cả rác bị trộn lẫn đều khó xử lý, thậm chí mất khả năng tái chế.
                    - Gây tắc nghẽn hệ thống thu gom, tiêu tốn ngân sách nhà nước.
                    - Tăng rủi ro cháy nổ nếu rác pin, rác điện tử bị chôn lấp chung với rác hữu cơ.
                    - Ảnh hưởng sức khỏe cộng đồng, ô nhiễm đất – nước – không khí.

                    **4. Hướng dẫn cách phân loại rác tại nguồn đơn giản**

                    - **Bước 1**: Chuẩn bị ít nhất 2-3 thùng hoặc túi đựng rác riêng biệt có nhãn rõ ràng: Hữu cơ – Tái chế – Khác.
                    - **Bước 2**: Rửa sạch vỏ hộp, lon, chai nhựa trước khi cho vào thùng tái chế.
                    - **Bước 3**: Tập thói quen hàng ngày, hướng dẫn cả người thân và trẻ nhỏ cùng thực hiện.
                    - **Bước 4**: Giao rác cho đơn vị thu gom đúng lịch, đúng loại.

                    **5. Vai trò của người dân trong hệ thống phân loại**

                    Dù chính quyền có đầu tư hệ thống xử lý hiện đại, nhưng nếu rác không được phân loại tại nguồn, mọi công nghệ phía sau đều trở nên kém hiệu quả. Mỗi người dân chính là mắt xích đầu tiên – và cũng là quan trọng nhất – trong chuỗi quản lý rác bền vững.

                    **6. Ví dụ thành công từ các nước phát triển**

                    - **Nhật Bản**: Mỗi thành phố có từ 7–10 loại phân loại rác khác nhau, người dân phải làm theo hướng dẫn chi tiết.
                    - **Đức**: Áp dụng hệ thống thùng rác màu sắc, người dân bị phạt nếu không phân loại đúng.
                    - **Hàn Quốc**: Có hệ thống cân rác, người dân bị tính phí nếu xả nhiều rác không tái chế.

                    **7. Việt Nam đã triển khai phân loại tại nguồn ra sao?**

                    Một số địa phương như TP.HCM, Hà Nội, Đà Nẵng đã triển khai thí điểm mô hình phân loại rác tại hộ gia đình, kèm theo chính sách thu gom riêng. Tuy nhiên, rào cản lớn nhất vẫn là thói quen của người dân và sự thiếu đồng bộ trong thu gom, vận chuyển.

                    **8. Làm sao để phân loại rác trở thành thói quen?**

                    - Bắt đầu từ gia đình: dùng thùng rác đôi, ghi nhãn rõ ràng, nhắc nhở thành viên.
                    - Trường học cần đưa vào chương trình học và hoạt động ngoại khóa.
                    - Doanh nghiệp cần có quy định nội bộ, hướng dẫn nhân viên.
                    - Cộng đồng dân cư nên tổ chức ngày hội “sống xanh – phân loại rác” để lan tỏa ý thức.

                    **9. Tầm nhìn dài hạn – từ hành động nhỏ đến thay đổi lớn**

                    Khi người dân phân loại rác tốt, chính quyền có thể dễ dàng tổ chức mô hình tái chế, compost hóa, sản xuất điện từ rác… tạo thành một vòng tuần hoàn xanh. Mỗi người dân trở thành một phần của giải pháp – chứ không phải nguyên nhân của ô nhiễm.

                    **Kết luận:** Phân loại rác tại nguồn không chỉ là nghĩa vụ, mà còn là hành động thiết thực thể hiện trách nhiệm của mỗi người với môi trường. Từ việc đặt thêm một thùng rác nhỏ, từ việc rửa sạch một vỏ hộp sữa trước khi vứt đi… chúng ta đang đóng góp vào sự chuyển mình của xã hội văn minh, sạch đẹp và phát triển bền vững.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Ftamquantrong.jpg?alt=media",
                    AuthorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogTypeId = "11111111-1111-1111-1111-111111111111",
                    Status = "Published"
                },
                new Blogs
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Title = "Hỏi đáp về phân loại và xử lý rác thải nhựa",
                    Content = @"Rác thải nhựa đang là một trong những vấn đề môi trường nóng nhất hiện nay. Tuy nhiên, nhiều người vẫn chưa thực sự hiểu rõ cách phân loại và xử lý rác nhựa đúng cách. Trong bài viết này, chúng tôi tổng hợp các câu hỏi thường gặp cùng với câu trả lời từ chuyên gia nhằm giúp bạn hiểu rõ hơn và dễ dàng áp dụng trong đời sống hằng ngày.

                    **Câu hỏi 1: Nhựa nào có thể tái chế?**

                    Không phải tất cả các loại nhựa đều có thể tái chế. Nhựa thường được phân loại theo ký hiệu số từ 1 đến 7. Trong đó:
                    - **PET (1)** – Chai nước suối, nước ngọt: Có thể tái chế.
                    - **HDPE (2)** – Can nhựa, chai sữa: Tái chế tốt.
                    - **PVC (3)** – Ống nước, bao bì: Khó tái chế, thường không thu gom.
                    - **LDPE (4)** – Túi nylon mềm, màng bọc thực phẩm: Tái chế hạn chế.
                    - **PP (5)** – Hộp đựng thực phẩm, ống hút: Có thể tái chế nếu sạch.
                    - **PS (6)** – Hộp xốp, muỗng nhựa: Rất khó tái chế.
                    - **Other (7)** – Các loại nhựa phức hợp: Không tái chế được.

                    **Câu hỏi 2: Làm sao biết sản phẩm làm từ nhựa gì?**

                    Hãy nhìn dưới đáy sản phẩm – đa số có ký hiệu hình tam giác với số từ 1–7. Nếu không có, bạn có thể tra cứu thông tin sản phẩm hoặc hỏi nhà sản xuất.

                    **Câu hỏi 3: Có cần rửa sạch chai/lọ nhựa trước khi bỏ vào thùng tái chế không?**

                    **Có.** Nhựa bẩn hoặc lẫn thực phẩm sẽ bị loại khỏi quy trình tái chế. Vì vậy, hãy rửa sạch, để khô trước khi bỏ vào thùng phân loại. Việc này không mất nhiều thời gian nhưng cực kỳ quan trọng.

                    **Câu hỏi 4: Có thể đốt nhựa để xử lý không?**

                    **Không nên.** Khi bị đốt, nhựa phát sinh các khí độc như dioxin, furan… gây ung thư và ô nhiễm không khí. Việc đốt nhựa chỉ nên được thực hiện trong nhà máy chuyên dụng có hệ thống lọc khí.

                    **Câu hỏi 5: Nhựa mềm như túi nylon có tái chế được không?**

                    Một số túi nylon có thể tái chế, nhưng điều kiện rất khó: túi phải sạch, không nhăn, không lẫn tạp chất. Vì thế, giải pháp tốt nhất là **giảm sử dụng** và thay bằng túi vải, túi giấy.

                    **Câu hỏi 6: Vỏ hộp sữa, hộp giấy có lớp nhựa có tái chế được không?**

                    Các hộp này gọi là bao bì nhiều lớp (gồm giấy + nhựa + nhôm). Rất khó tái chế tại Việt Nam do thiếu công nghệ tách lớp. Tuy nhiên, một số chương trình như **Tetra Pak** có hỗ trợ thu gom tại điểm tái chế riêng.

                    **Câu hỏi 7: Tôi không có điểm thu gom rác tái chế gần nhà, phải làm sao?**

                    Bạn có thể:
                    - Tự gom và mang đến điểm thu hồi của các tổ chức môi trường như Greenlife, Tái Chế Xanh,…
                    - Liên hệ tổ dân phố để biết thông tin ngày thu gom rác tái chế định kỳ.
                    - Lưu trữ rác sạch trong nhà và chờ dịp giao nộp tập trung.

                    **Câu hỏi 8: Các sản phẩm nhựa có thể tái sử dụng tại nhà không?**

                    Hoàn toàn có thể. Một số gợi ý:
                    - Dùng chai nhựa làm bình tưới cây.
                    - Dùng hộp nhựa đựng dụng cụ học tập.
                    - Làm chậu trồng rau từ can nhựa cũ.
                    Tuy nhiên, cần hạn chế tái sử dụng các chai nhựa mỏng (PET) để đựng thực phẩm nóng vì chúng có thể thôi nhiễm chất độc hại.

                    **Câu hỏi 9: Phân loại nhựa tại nhà có phức tạp không?**

                    Không. Bạn chỉ cần:
                    - Dành một thùng riêng cho rác nhựa sạch.
                    - Rửa sơ qua, để khô.
                    - Nếu có thể, phân ra theo nhóm: chai – hộp – túi – vật dụng khác.
                    Sự đều đặn sẽ tạo thành thói quen, và cả gia đình có thể tham gia.

                    **Câu hỏi 10: Tôi thấy có nơi thu gom, có nơi không, vậy phân loại để làm gì?**

                    Ngay cả khi chưa có hệ thống thu gom đồng bộ, việc phân loại tại nhà vẫn **cực kỳ quan trọng** vì:
                    - Giúp rác sạch, không làm nhiễm bẩn rác tái chế khác.
                    - Có thể bán cho người thu gom ve chai.
                    - Dễ tổ chức giao nộp khi có chương trình tái chế đến tận nơi.

                    **Câu hỏi 11: Có tổ chức nào hỗ trợ xử lý rác nhựa không?**

                    Rất nhiều tổ chức, startup và chiến dịch môi trường tại Việt Nam đang triển khai chương trình thu gom nhựa như:
                    - **Greenhub** – tổ chức các ngày đổi rác lấy cây.
                    - **Zero Waste Việt Nam** – hướng dẫn sống không rác thải.
                    - **Rác Thải Nhựa Việt Nam** – có hệ thống điểm thu gom ở nhiều tỉnh.

                    **Câu hỏi 12: Làm sao giúp trẻ nhỏ hiểu về rác nhựa?**

                    Bạn có thể:
                    - Cùng con phân loại rác sau bữa ăn.
                    - Dùng mô hình trò chơi ghép hình, tô màu rác đúng – sai.
                    - Kể cho con những câu chuyện về động vật bị ảnh hưởng bởi rác nhựa như rùa biển, cá voi…

                    **Tổng kết:**
                    Việc phân loại và xử lý rác thải nhựa không chỉ là việc của nhà nước, của hệ thống thu gom, mà còn là trách nhiệm của **mỗi người dân**. Bằng những hành động nhỏ như rửa sạch vỏ chai, tách riêng rác nhựa và giảm dùng đồ nhựa một lần, bạn đang góp phần tạo nên một Việt Nam xanh – sạch – đẹp.

                    Hãy bắt đầu hôm nay, từ chính ngôi nhà của bạn!",
                    ImageUrl = null,
                    AuthorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    BlogTypeId = "55555555-5555-5555-5555-555555555555",
                    Status = "Published"
                }

            );

            builder.Entity<RecycleLocations>().HasData(
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trung tâm Tái chế Hà Nội",
                    Address = "Số 10 Đường Láng, Đống Đa, Hà Nội",
                    ContactNumber = "0243-123-4567",
                    Description = "Tiếp nhận rác thải sinh hoạt và điện tử.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 0, 0, TimeSpan.FromHours(7)),
                    Longitude = "105.8342",
                    Latitude = "21.0278",
                    WasteTypeId = "11111111-1111-1111-1111-111111111111",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trạm Tái chế Quận 1",
                    Address = "25 Nguyễn Huệ, Quận 1, TP.HCM",
                    ContactNumber = "0283-222-3344",
                    Description = "Chuyên xử lý rác nhựa và kim loại.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 7, 30, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 16, 30, 0, TimeSpan.FromHours(7)),
                    Longitude = "106.6951",
                    Latitude = "10.7769",
                    WasteTypeId = "22222222-2222-2222-2222-222222222222",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trung tâm Xử lý Rác Đà Nẵng",
                    Address = "101 Lê Duẩn, Hải Châu, Đà Nẵng",
                    ContactNumber = "0236-888-9999",
                    Description = "Xử lý rác hữu cơ và tái sử dụng phân bón.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 0, 0, TimeSpan.FromHours(7)),
                    Longitude = "108.2186",
                    Latitude = "16.0544",
                    WasteTypeId = "33333333-3333-3333-3333-333333333333",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trung tâm Tái chế Cần Thơ",
                    Address = "45 Mậu Thân, Ninh Kiều, Cần Thơ",
                    ContactNumber = "0292-777-8888",
                    Description = "Chuyên thu gom pin và thiết bị điện tử.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 9, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 18, 0, 0, TimeSpan.FromHours(7)),
                    Longitude = "105.7461",
                    Latitude = "10.0343",
                    WasteTypeId = "44444444-4444-4444-4444-444444444444",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trạm Tái chế Hải Phòng",
                    Address = "89 Trần Phú, Ngô Quyền, Hải Phòng",
                    ContactNumber = "0225-666-7777",
                    Description = "Tiếp nhận rác công nghiệp nhẹ.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 30, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 30, 0, TimeSpan.FromHours(7)),
                    Latitude = "20.8449",
                    Longitude = "106.6881",
                    WasteTypeId = "55555555-5555-5555-5555-555555555555",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Điểm thu gom Quận Thủ Đức",
                    Address = "12 Võ Văn Ngân, Thủ Đức, TP.HCM",
                    ContactNumber = "0283-999-0000",
                    Description = "Nhận chai nhựa, lon nhôm và giấy vụn.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 15, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 15, 0, TimeSpan.FromHours(7)),
                    Latitude = "10.8432",
                    Longitude = "106.8037",
                    WasteTypeId = "11111111-1111-1111-1111-111111111111",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Nhà máy Tái chế Bình Dương",
                    Address = "KCN VSIP, Thuận An, Bình Dương",
                    ContactNumber = "0274-888-1111",
                    Description = "Nhà máy tái chế quy mô lớn.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 7, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 0, 0, TimeSpan.FromHours(7)),
                    Latitude = "10.9626",
                    Longitude = "106.6511",
                    WasteTypeId = "22222222-2222-2222-2222-222222222222",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Điểm phân loại rác Nha Trang",
                    Address = "200 Trần Phú, TP. Nha Trang, Khánh Hòa",
                    ContactNumber = "0258-123-4567",
                    Description = "Phân loại rác thải sinh hoạt.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 9, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 18, 0, 0, TimeSpan.FromHours(7)),
                    Latitude = "12.2384",
                    Longitude = "109.1967",
                    WasteTypeId = "33333333-3333-3333-3333-333333333333",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trung tâm Môi trường Huế",
                    Address = "15 Hùng Vương, TP. Huế",
                    ContactNumber = "0234-654-3210",
                    Description = "Tái chế vật liệu xây dựng và thủy tinh.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 0, 0, TimeSpan.FromHours(7)),
                    Latitude = "16.4637",
                    Longitude = "107.5909",
                    WasteTypeId = "44444444-4444-4444-4444-444444444444",
                    CreatedBy = "System"
                },
                new RecycleLocations
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Trạm tái chế Biên Hòa",
                    Address = "Khu phố 1, Trảng Dài, Biên Hòa, Đồng Nai",
                    ContactNumber = "0251-333-2222",
                    Description = "Thu gom và xử lý rác điện tử.",
                    OpeningTime = new DateTimeOffset(2023, 1, 1, 8, 0, 0, TimeSpan.FromHours(7)),
                    ClosingTime = new DateTimeOffset(2023, 1, 1, 17, 0, 0, TimeSpan.FromHours(7)),
                    Latitude = "10.9507",
                    Longitude = "106.8352",
                    WasteTypeId = "55555555-5555-5555-5555-555555555555",
                    CreatedBy = "System"
                }
            );

            builder.Entity<Brands>().HasData(
                new Brands
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Green Life",
                    Description = "Công ty cung cấp giải pháp bền vững trong thực phẩm và đồ uống tại Việt Nam.",
                    LogoUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fgreenlielogo.png?alt=media",
                    WebsiteUrl = "https://greenlifecorp.com.vn"
                },
                new Brands
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "GreenHub",
                    Description = "Trung tâm Hỗ trợ Phát triển Xanh – tổ chức thúc đẩy lối sống xanh và giảm rác thải tại Việt Nam.",
                    LogoUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Fgreenhublogo.svg?alt=media",
                    WebsiteUrl = "https://greenhub.org.vn"
                },
                new Brands
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "ReForm Plastic",
                    Description = "Dự án tái chế nhựa của Evergreen Labs tại Việt Nam, hỗ trợ thu gom và tái chế nhựa tại cộng đồng.",
                    LogoUrl = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/EXE201%2Freformlogo.png?alt=media",
                    WebsiteUrl = "https://evergreenlabs.org/reformplastic"
                }
            );
            builder.Entity<TransactionTypes>().HasData(
               new TransactionTypes(
                   id: "11111111-0000-0000-0000-00000001",
                   name: "Donate Campaign",
                   description: "Ủng hộ chiến dịch",
                   category: "IN"
               ),
               new TransactionTypes(
                   id: "11111111-0000-0000-0000-00000002",
                   name: "Donate Single",
                   description: "Ủng hộ đơn lẻ",
                   category: "IN"
               ),
               new TransactionTypes(
                   id: "11111111-0000-0000-0000-00000003",
                   name: "Top Up",
                   description: "Nạp tiền",
                   category: "IN"
               ),
               new TransactionTypes(
                   id: "11111111-0000-0000-0000-00000004",
                   name: "Withdraw",
                   description: "Rút tiền",
                   category: "OUT"
               )
             );
        }
    }
}
