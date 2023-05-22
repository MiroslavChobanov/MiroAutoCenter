﻿using System;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek)
    {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-1 * diff).Date;
    }
}
