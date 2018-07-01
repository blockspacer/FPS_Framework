/*
	Manages an unordered list of ItemData
*/

using System;
using System.Collections.Generic;
using Godot;

public class Inventory : IHasItem {
	List<ItemData> items;

	public Inventory(){
		items = new List<ItemData>();
	}

	public void Init(List<ItemData> items){
		this.items = items;
	}

	// Doesn't change value.
	public ItemData GetItem(int index){
		if(index < 0 || index >= items.Count){
			return null;
		}
		return items[index];
	}

	// Changes value.
	public ItemData RetrieveItem(int index, int quantity = 1){
		if(GetItem(index) == null){
			return null;
		}
		ItemData ret = items[index].Remove(quantity);
		if(items[index].quantity == 0){
			items.RemoveRange(index, 1);
		}
		return ret;
	}

	/* Returns item based on matching type and name. Returns -1 if not found.*/
	public int IndexOf(Item.Types type, string name){
		for(int i = 0; i < items.Count; i++){
			ItemData item = items[i];
			if(item.type == type && item.name == name){
				return i;
			}
		}
		return -1;
	}

	public string ToString(){
		string ret = "Inventory:\n";
		foreach(ItemData item in items){
			ret += item.ToString() + "\n";
		}
		return ret;
	}

	/* Returns true item with matching name, ignoring type. */
	public bool HasItem(string itemName){
		foreach(ItemData item in items){
			if(itemName == item.name){
				return true;
			}
		}
		return false;
	}

	public string ItemInfo(){
		return ToString();
	}

	public int ReceiveItem(Item item){
		ItemData data = item.GetData();
		int index = IndexOf(data.type, data.name);
		if(index == -1){
			items.Add(data);
			return 0;
		}
		else{
			return items[index].Add(data);
		}
	}

	public Item PrimaryItem(){
		return null;
	}

}