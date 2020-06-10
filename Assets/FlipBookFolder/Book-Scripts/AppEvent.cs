using System.Collections;
using System.Collections.Generic;
using System;

public static class AppEvent
{
    public static event EventHandler CloseBook;
    public static event EventHandler OpenBook;

    public static void ClosedBookFun()
    {
        if (CloseBook != null)
            CloseBook(new object(), new EventArgs());
    }
    public static void OpenBookFun()
    {
        if (OpenBook != null)
            OpenBook(new object(), new EventArgs());
    }

}
