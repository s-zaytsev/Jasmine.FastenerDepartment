package com.szaytsev.jasminefastenerdepartment.data.converters

import androidx.room.TypeConverter
import java.time.ZonedDateTime

class ZonedDateTimeConverter {
    @TypeConverter
    fun fromString(value: String?): ZonedDateTime {
        return ZonedDateTime.parse(value)
    }

    @TypeConverter
    fun toString(date: ZonedDateTime): String {
        return date.toOffsetDateTime().toString()
    }
}