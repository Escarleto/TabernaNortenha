using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;
using System;

public class DishHandler : MonoBehaviour
{
    private List<FoodController.FoodType?> Dish = new List<FoodController.FoodType?>();
    private List<FoodController> FoodsInDish = new List<FoodController>();

    [SerializeField] private GameObject[] FinalDishes;
    private FoodController LastFood;
    private Dish LastDish;

    private int Index;
    public bool DishComplete { get; private set; }
    [System.NonSerialized] public bool IsVegetarianDish = true;

    public bool BuildDish(FoodController Food)
    {
        if (DishComplete) return false;

        FoodController.FoodType FoodType = Food.Type;
        if (!IsValid(FoodType)) return false;

        // garante que a lista tenha espaço suficiente
        if (Dish.Count <= Index)
            Dish.Add(FoodType);
        else
            Dish[Index] = FoodType;

        FoodsInDish.Add(Food);

        if (LastFood != null)
            Food.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                LastFood.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

        LastFood = Food;

        if (Index == 2 && FoodType == FoodController.FoodType.Bread)
        {
            Index = 4;
            return true;
        }

        if ((Index == 5 || Index == 6) && FoodType == FoodController.FoodType.Sauce)
        {
            Finish();
            return true;
        }

        Index++;
        return true;
    }

    private bool IsValid(FoodController.FoodType Food) => Index switch
    {
        0 or 3 => Food == FoodController.FoodType.Bread,

        1 or 2 => Food == FoodController.FoodType.Bread ||
                  Food == FoodController.FoodType.Filling,

        4 => Food == FoodController.FoodType.Topping,

        5 or 6 => Food == FoodController.FoodType.Topping ||
                  Food == FoodController.FoodType.Sauce,

        7 => Food == FoodController.FoodType.Sauce,

        _ => false
    };

    private void Finish()
    {
        DishComplete = true;

        var FinalDishIndex =
            IsVegetarianDish == true ? 0 :
            (Dish.Count > 5 && Dish[5] == FoodController.FoodType.Topping && IsVegetarianDish == false) ? 1 :
            2;

        LastDish = Instantiate(
            FinalDishes[FinalDishIndex],
            transform.position,
            Quaternion.identity,
            transform
        ).GetComponent<Dish>();

        UIManager.Instance.CurrentDish = LastDish;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        foreach (var Food in FoodsInDish)
        {
            if (Food != null)
                Destroy(Food.gameObject);
        }

        FoodsInDish.Clear();

        Dish.Clear();
        Index = 0;
        LastFood = null;
    }

    public void ResetDish()
    {
        IsVegetarianDish = true;
        DishComplete = false;

        if (LastDish != null)
            Destroy(LastDish.gameObject);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        Dish.Clear();
        Index = 0;
        LastFood = null;
    }
}