using ASPNetCoreApp.Domain.DTO;

namespace ASPNetCoreApp.Domain.DTO
{
    /// <summary>
    /// Описание канала передачи товаров
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Заказ
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Ссылка на картинку
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Описание брендов
        /// </summary>
        public BrandDTO Brand { get; set; }

        /// <summary>
        /// Секция
        /// </summary>
        public SectionDTO Section { get; set; }

    }
}
