// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;

////
// this is old method of explicitly defining delegate with first argument as source 
// and second argument as EventArg concrete class carrying event information
////
// public delegate PriceChangedEventHandler(object source, PriceChangeEventArgs e);

////
// this class is exclusively used for conveying information about an event
// it always inherits from the base class EventArgs which has only a single static field Empty
////
public class PriceChangeEventArgs : EventArgs
{
    public readonly int OldPrice;
    public readonly int NewPrice;
    
    public PriceChangeEventArgs(int oldPrice, int newPrice)
    {
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class WatchStocks
{
    public void PrintStockChange(object sender, PriceChangeEventArgs e) => Console.WriteLine($"OldPrice : {e.OldPrice}; NewPrice : {e.NewPrice}\n");

    public void BuyOrSellStocks(object send, PriceChangeEventArgs e)
    {
        if(e.OldPrice > e.NewPrice)
        {
            Console.WriteLine("Sell Stocks now!!\n");
        }
        else
        {
            Console.WriteLine("Take it easy, hold the stocks\n");
        }
    }
}

public class Stocks
{
    public event EventHandler<PriceChangeEventArgs> PriceChange;
    
    private int price;
    
    public int Price 
    {
      get => price;
      set
      {
          if (price == value) 
            return;
          int oldPrice = price;
          price = value;
          OnPriceChange(new PriceChangeEventArgs(oldPrice, price));
      }
    }
    // this method needs to be of this signature to be extended with name matching the delegate name
    public virtual void  OnPriceChange(PriceChangeEventArgs e)
    {
        PriceChange?.Invoke(this,e); // ?. is used to ensure thread safety. If we use if (!=null) it may not be thread safe!!
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        WatchStocks wd = new WatchStocks();
        Stocks stocks = new Stocks();
        stocks.PriceChange += wd.PrintStockChange;
        stocks.PriceChange += wd.BuyOrSellStocks;
        
        int[] prices = {100, 50, 150, 200, 10};
        foreach(var price in prices)
        {
            stocks.Price = price;
        }
    }
}