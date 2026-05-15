package com.szaytsev.jasminefastenerdepartment.data.serializers

import com.szaytsev.jasminefastenerdepartment.data.models.ProductChangeReasonCode
import kotlinx.serialization.KSerializer
import kotlinx.serialization.descriptors.PrimitiveKind
import kotlinx.serialization.descriptors.PrimitiveSerialDescriptor
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder

object ProductChangeReasonCodeSerializer : KSerializer<ProductChangeReasonCode> {
    override val descriptor: SerialDescriptor =
        PrimitiveSerialDescriptor("changeReasonCode", PrimitiveKind.STRING)

    override fun deserialize(decoder: Decoder): ProductChangeReasonCode {
        val a = ProductChangeReasonCode.fromString(decoder.decodeString())
        return  a;
    }

    override fun serialize(encoder: Encoder, value: ProductChangeReasonCode) {
        encoder.encodeInt(value.ordinal)
    }
}