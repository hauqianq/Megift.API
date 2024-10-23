using Bogus;

namespace Megift.API.Models
{
    public static class DataSeeder
    {
        public static void SeedData(MeGiftContext context)
        {
            // Kiểm tra nếu dữ liệu đã tồn tại, thì không seed lại
            if (context.Customers.Any() || context.Products.Any() || context.Orders.Any() || context.Reviews.Any())
            {
                return;
            }

            Randomizer.Seed = new Random(8675309); // Đặt seed để tạo dữ liệu giả có thể dự đoán

            #region Seed Customers
            var avartars = new[]
            {
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F8e904c57-3d18-407f-aa8e-a47b8a27aa3a_1.jpg?alt=media&token=cee57690-3934-499e-9b16-5baf694a0409",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Ffa9eadca-99db-43a9-998a-2d77277314ca_2.jpg?alt=media&token=39d17699-4c5b-4d9a-b935-f834ada4cbfb",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F050479d5-d1c3-4d59-89fd-10a65f1f503b_3.jpg?alt=media&token=e735b87a-269b-4bc2-a796-16f061843747",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fc0190e1c-7132-43a1-9fb6-452e25afb423_4.jpg?alt=media&token=a2f4f91b-a43c-4cfd-a789-14f03f761d2e",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F3b70004c-653a-4144-955e-a541be96ae15_5.jpg?alt=media&token=f0a984b9-1348-4fb2-b7be-96f97ae8650c",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F869a14e6-1529-4ff2-8381-8e6eefe13d5c_6.jpg?alt=media&token=64424582-a2f8-44ee-8562-7fbb4ea34737",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F092ba82c-c6f2-41f6-92c6-590ff6e4ea18_7.jpg?alt=media&token=cc4faffe-9452-4609-865f-8fd1132e7d84",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F613e7c16-4304-4224-85f7-4582a18c4c7b_8.jpg?alt=media&token=04081a0a-4163-445b-82b4-42c1f9488273",
                  "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fdd7ef4e1-e60a-4489-b6ff-61ac91673e48_9.jpg?alt=media&token=beb00468-78f7-47aa-a7ca-3098bc8d5487"
            };

            var fakerCustomer = new Faker<Customer>()
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.ShippingAddress, f => f.Address.FullAddress())
                .RuleFor(p => p.Image, f => f.PickRandom(avartars)); // Chọn URL ngẫu nhiên từ danh sách

            var customers = fakerCustomer.Generate(30);
            context.Customers.AddRange(customers);
            context.SaveChanges(); // Lưu khách hàng để có CustomerId
            #endregion

            #region Seed Products
            // Danh sách các URL hình ảnh giả định cho sản phẩm
            var imageUrls = new[]
            {
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F6ef778bc-0d42-4dab-a9b0-aa2e3d576fb2_images%20%281%29.jpg?alt=media&token=788a0fc1-545a-4e81-8c25-d57f0e2ba828",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F2e2ea07c-abfe-40cb-98c3-63d9e51b9723_images%20%282%29.jpg?alt=media&token=c9f66957-bef6-4dca-8d00-c66877761633",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F5c2c32a1-58a2-424c-8837-f6c7df295593_images%20%283%29.jpg?alt=media&token=22c69ea1-3c8a-451b-8342-fab15a05ea83",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fa2a6ec37-4298-4fa8-84e7-f38dc140d63c_images%20%284%29.jpg?alt=media&token=db349bcc-4e1c-4918-aed4-2c94e857e107",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fd69d1c6e-0c76-45e8-9823-4dedfe876f23_images%20%285%29.jpg?alt=media&token=34f5be6a-d547-4267-beb9-a0b82ffced8b",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Feb18273e-b4f2-4e0a-bffb-cb6278ee7ab3_images%20%286%29.jpg?alt=media&token=404b74ad-57e5-4dad-a94b-ea7bb54fe276",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F262e8d0c-3ea4-4a93-8835-eb9df860cb7b_images%20%287%29.jpg?alt=media&token=64f529bd-f25b-44ad-bff6-6f6c2eaead68",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F0be3772c-e7be-4c6a-a9ef-c8a629cec1e6_images%20%288%29.jpg?alt=media&token=2a796cc3-03b9-4ed6-91ff-07de5bbc9d8d",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fd8548011-dc57-45d5-b91a-1e5f1cc1e85a_images%20%289%29.jpg?alt=media&token=0c8d07ea-5c38-416c-ae1e-c250e2cc7b28",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fa0fdced3-6f94-4d56-8f25-63bd08f2e566_images%20%2810%29.jpg?alt=media&token=c3dd44bf-1089-4dcf-8bfa-3d9c53aa23b9",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fbc4cf2ec-d8a1-459d-9d1f-f14eaa336e6c_images%20%2811%29.jpg?alt=media&token=cf52f114-bbd1-45a3-971b-a9c8c4b2cfa9",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F56ae104f-060a-4f03-9dec-d7189de3e24d_images%20%2812%29.jpg?alt=media&token=b82b0f4b-237d-4bdd-8513-ef2d038af911",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1a8fb472-945f-492b-9adc-4a827293abe9_images%20%2813%29.jpg?alt=media&token=048e7b24-8f59-4290-be36-674256951693",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F726672b9-5d33-48b4-abf4-68b87e109209_images%20%2814%29.jpg?alt=media&token=0861930b-7711-4a2d-bf74-3efaf9cd4df7",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F635b691b-3bc9-4c2f-9c0f-0038df26389e_images%20%2815%29.jpg?alt=media&token=efbf6ebd-cf99-48de-b73d-4dc8d5d3cf03",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Feb26e7a7-607d-445f-aa3e-ff6118c829af_images%20%2816%29.jpg?alt=media&token=592e6c4f-e388-41d0-bb27-aa551a135482",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F60d2c6a2-c789-42b8-b5d6-92172fadd4f0_images%20%2817%29.jpg?alt=media&token=7455ed59-3e7d-46bf-afbe-92dd0f311b22",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F04e7dfee-36a4-4f15-a32e-512361e01da6_images%20%2818%29.jpg?alt=media&token=4cb18722-8346-493f-ad50-803cd73e3294",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F485df9f7-044f-4855-8dbe-257a2e74310b_images%20%2819%29.jpg?alt=media&token=d4ac81fb-35b7-4599-8e8f-a0f36c32a500",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F07f26986-6db1-4612-90b1-318144e0689e_images%20%2820%29.jpg?alt=media&token=8a9a8fcc-655b-4a12-999b-606bf2254599",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Ffa95dcd5-2f57-4dfe-b7f4-ebbb89f6e6f0_images.jpg?alt=media&token=ae5b4ce7-b5c7-40cf-92f5-2dac3ce50cd2",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F4c7e10d3-662c-452c-9e8f-8bc61bab6dcb_t%E1%BA%A3i%20xu%E1%BB%91ng%20%281%29.jpg?alt=media&token=08a4bb7b-ac53-4bb9-a6ff-3b7b1db4d03d",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F6595022e-7d95-45a5-bd87-31a0d2fa7a79_t%E1%BA%A3i%20xu%E1%BB%91ng%20%282%29.jpg?alt=media&token=12ad66a6-2e1e-4b95-b45f-befad6f73028",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F2ff304ce-b651-4844-9355-6c6610bb1de2_t%E1%BA%A3i%20xu%E1%BB%91ng%20%283%29.jpg?alt=media&token=6c75176c-b9e0-4b76-92be-20dacab92fc0",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Ff59ea83c-7d1e-4612-a30f-a408ed7d9534_t%E1%BA%A3i%20xu%E1%BB%91ng%20%284%29.jpg?alt=media&token=de9f7ad0-b970-4dfc-bf31-84d2e61862dd",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F97048861-d122-4d73-bb29-337b4e1fd613_t%E1%BA%A3i%20xu%E1%BB%91ng%20%285%29.jpg?alt=media&token=5972bcbf-664b-45e4-a678-de5a074ef35e",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F6d8a527e-8b4a-4f94-8dc4-336d8ca5c45d_t%E1%BA%A3i%20xu%E1%BB%91ng%20%286%29.jpg?alt=media&token=8e4ad313-ed5e-43a3-9bff-437fb49881ba",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2Fc345f429-8f73-42da-8793-eacefb61ce04_t%E1%BA%A3i%20xu%E1%BB%91ng%20%287%29.jpg?alt=media&token=18007fbc-3ced-4fc6-a357-626011ea0b28",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F7a6ffefb-138f-4461-a99c-1f463ed1be93_t%E1%BA%A3i%20xu%E1%BB%91ng%20%288%29.jpg?alt=media&token=b50c6499-2dfa-4eb8-8c7a-b065412969ed",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F7625059d-3505-470b-8078-6120b0a91aa1_t%E1%BA%A3i%20xu%E1%BB%91ng%20%289%29.jpg?alt=media&token=2021f2cf-5302-44f4-a925-edf6be68606a",
                "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F368de5fe-a4c1-47f6-abcc-a8fb09dab7e0_t%E1%BA%A3i%20xu%E1%BB%91ng.jpg?alt=media&token=5f81b6c6-57b5-4cfb-83ac-6cc4975e68fb"
            };

            var fakerProduct = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Sku, f => f.Commerce.Ean13()) // EAN13 giả lập cho SKU
                .RuleFor(p => p.Price, f => f.Random.Long(100_000, 500_000)) // Giá nằm trong khoảng 100.000 - 500.000 VND
                .RuleFor(p => p.Stock, f => f.Random.Int(0, 100).ToString()) // Số lượng tồn kho
                .RuleFor(p => p.Image, f => f.PickRandom(imageUrls)); // Chọn URL ngẫu nhiên từ danh sách

            var products = fakerProduct.Generate(30);
            context.Products.AddRange(products);
            context.SaveChanges();
            #endregion

            #region Seed Orders
            var fakerOrder = new Faker<Order>()
                .RuleFor(o => o.OrderDate, f => f.Date.Past())
                .RuleFor(o => o.Total, f => f.Finance.Amount())
                .RuleFor(o => o.Status, f => f.PickRandom(new[] { "Pending", "Completed", "Shipped" }))
                .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).Id) // Sử dụng ID của customer đã seed
                .RuleFor(o => o.ProductId, f => f.PickRandom(products).Id); // Sử dụng ID của product đã seed

            var orders = fakerOrder.Generate(30);
            context.Orders.AddRange(orders);
            context.SaveChanges(); // Lưu đơn hàng vào cơ sở dữ liệu
            #endregion

            #region Seed Reviews
            var fakerReview = new Faker<Review>()
                .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
                .RuleFor(r => r.CustomerId, f => f.PickRandom(customers).Id); // Sử dụng ID của customer đã seed

            var reviews = fakerReview.Generate(30);
            context.Reviews.AddRange(reviews);
            context.SaveChanges(); // Lưu đánh giá vào cơ sở dữ liệu
            #endregion
        }
    }
}
