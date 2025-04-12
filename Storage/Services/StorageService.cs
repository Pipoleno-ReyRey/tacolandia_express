using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;

namespace Storage.Services
{
    public class StorageService
    {
        private readonly StorageDB storageDB;

        public StorageService(StorageDB StorageDB) 
        {
            storageDB = StorageDB;
        }

        public async Task<List<Items>> GetItems()
        {
            try
            {
                return await storageDB.items.ToListAsync();
            }
            catch (Exception ex) 
            {
                return new List<Items>() { new Items() { id = null, name = ex.Message } };
            }
            
        }

        public async Task<Items> PostItem(Items item){
            try{
                if(await storageDB.items.AnyAsync(it => it.name == item.name)){
                    return new Items() { id = null, name = "el item ya existe en el almacen" };
                } else{
                    await storageDB.items.AddAsync(item);
                    await storageDB.SaveChangesAsync();
                    return item;
                }
                
            } catch(Exception error){
                return new Items() { id = null, name = error.InnerException?.Message };
            }
        }

        public async Task<Items> UpdateItem(ItemDTO itemDTO)
        {
            try
            {
                var item = await storageDB.items.FirstOrDefaultAsync(it => it.id == itemDTO.id && it.name == itemDTO.name);
                item!.amount += itemDTO.addAmount;
                await storageDB.SaveChangesAsync();
                return item;
            }catch(Exception error)
            {
                return new Items() { id = null, name = error.InnerException?.Message };
            }
        }
    }
}
