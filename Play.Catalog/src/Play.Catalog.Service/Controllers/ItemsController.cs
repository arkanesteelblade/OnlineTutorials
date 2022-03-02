using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Entities;
using System.Threading.Tasks;

namespace Play.Catalog.Service.Controllers
{
    // https://localhost:5001/Items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        /*private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        };*/
        private readonly ItemsRepository itemsRepository = new();

        [HttpGet]
        //public IEnumerable<ItemDto> Get()
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            //return items;
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        //GET /items/{id}
        [HttpGet("{id}")]
        //public ActionResult<ItemDto> GetById(Guid id)
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            //var item = items.Where(item => item.Id == id).SingleOrDefault();
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            //return item;
            return item.AsDto();
        }

        // POST /Items
        [HttpPost]
        //public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            //var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            //items.Add(item);

            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        //public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            /*var existingItem = items.Where(item => item.Id == id).SingleOrDefault();

            if (existingItem == null)
            {
                return NotFound();
            }

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items[index] = updatedItem;*/

            var existingItem = await itemsRepository.GetAsync(id);
            if (existingItem == null)
                return NotFound();

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            /*var index = items.FindIndex(existingItem => existingItem.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            items.RemoveAt(index);*/

            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(item.Id);

            return NoContent();
        }

        //Tutorial:  Youtube:  https://www.youtube.com/watch?v=CqCDOosvZIk
        //Stopped at: 1:15:59

    }
}