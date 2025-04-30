namespace Mango.Service.AuthAPI.Models.Dto
{
    // 使用者
    public class UserDto
    {
        // 使用者Id
        public string ID { get; set; }

        // 電子信箱
        public string Email { get; set; }

        // 姓名
        public string Name { get; set; }

        // 電話號碼
        public string PhoneNumber { get; set; }
    }
}
