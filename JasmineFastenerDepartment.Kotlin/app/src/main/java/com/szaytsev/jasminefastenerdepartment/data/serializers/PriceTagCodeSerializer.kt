package com.szaytsev.jasminefastenerdepartment.data.serializers

import com.szaytsev.jasminefastenerdepartment.data.models.ProductPriceTagCode
import kotlinx.serialization.KSerializer
import kotlinx.serialization.descriptors.PrimitiveKind
import kotlinx.serialization.descriptors.PrimitiveSerialDescriptor
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder

object PriceTagCodeSerializer : KSerializer<ProductPriceTagCode> {
    override val descriptor: SerialDescriptor =
        PrimitiveSerialDescriptor("priceTagCode", PrimitiveKind.STRING)

    override fun deserialize(decoder: Decoder): ProductPriceTagCode {
        val a = ProductPriceTagCode.fromString(decoder.decodeString())
        return  a;
    }

    override fun serialize(encoder: Encoder, value: ProductPriceTagCode) {
        encoder.encodeInt(value.ordinal)
    }
}