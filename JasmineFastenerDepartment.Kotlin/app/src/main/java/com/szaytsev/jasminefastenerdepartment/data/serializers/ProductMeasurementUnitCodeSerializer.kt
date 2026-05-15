package com.szaytsev.jasminefastenerdepartment.data.serializers

import com.szaytsev.jasminefastenerdepartment.data.models.ProductMeasurementUnitCode
import kotlinx.serialization.KSerializer
import kotlinx.serialization.descriptors.PrimitiveKind
import kotlinx.serialization.descriptors.PrimitiveSerialDescriptor
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder

object ProductMeasurementUnitCodeSerializer : KSerializer<ProductMeasurementUnitCode> {
    override val descriptor: SerialDescriptor =
        PrimitiveSerialDescriptor("priceTagCode", PrimitiveKind.STRING)

    override fun deserialize(decoder: Decoder): ProductMeasurementUnitCode {
        val a = ProductMeasurementUnitCode.fromString(decoder.decodeString())
        return  a;
    }

    override fun serialize(encoder: Encoder, value: ProductMeasurementUnitCode) {
        encoder.encodeInt(value.ordinal)
    }
}