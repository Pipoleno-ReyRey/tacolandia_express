using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services;

namespace Storage.Controllers
{
    [ApiController]
    [Route("storage/")]
    public class StorageController : ControllerBase
    {
        private readonly StorageService storageService;
        public StorageController(StorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems(){
            if((await storageService.GetItems())[0].id == null){
                return BadRequest((await storageService.GetItems())[0].name);
            } else{
                return Ok(await storageService.GetItems());
            }
        }

        [HttpPost("postItem")]
        public async Task<IActionResult> PostItems([FromBody] Items item)
        {
            var it = await storageService.PostItem(item);
            if (it.id == null)
            {
                var exception = it.name;
                return BadRequest(exception);
            }
            else
            {
                return Ok(it);
            }
        }

        [HttpPut("updateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDTO itemDTO)
        {
            var it = await storageService.UpdateItem(itemDTO);
            if (it.id == null)
            {
                var exception = it.name;
                return BadRequest(exception);
            }
            else
            {
                return Ok(it);
            }
        }
    }
}
