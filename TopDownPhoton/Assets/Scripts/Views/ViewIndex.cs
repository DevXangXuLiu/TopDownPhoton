using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView = 0,
    HomeView = 1,
    MatchView = 2
}

public class ViewConfig
{
    public static ViewIndex[] viewIndices = {
        ViewIndex.EmptyView,
        ViewIndex.HomeView,
        ViewIndex.MatchView
    };
}
