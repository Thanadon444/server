package com.example.topup

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val baseUrl = findViewById<EditText>(R.id.baseUrl)
        val token = findViewById<EditText>(R.id.token)
        val amount = findViewById<EditText>(R.id.amount)
        val result = findViewById<TextView>(R.id.result)
        val topUpButton = findViewById<Button>(R.id.topUpButton)

        topUpButton.setOnClickListener {
            val parsedAmount = amount.text.toString().toDoubleOrNull()
            if (parsedAmount == null) {
                result.text = "จำนวนเงินไม่ถูกต้อง"
                return@setOnClickListener
            }

            val retrofit = buildRetrofit(baseUrl.text.toString())
            val service = retrofit.create(TopUpApi::class.java)

            lifecycleScope.launch {
                runCatching {
                    service.topUp(
                        auth = "Bearer ${token.text}",
                        request = TopUpRequest(
                            amount = parsedAmount,
                            country = "LA",
                            appCountry = "TH"
                        )
                    )
                }.onSuccess {
                    result.text = "สำเร็จ=${it.success}, สกุลเงิน=${it.currency}, settle=${it.settlementCountry}"
                }.onFailure {
                    result.text = "ผิดพลาด: ${it.message}"
                }
            }
        }
    }

    private fun buildRetrofit(baseUrl: String): Retrofit {
        val logging = HttpLoggingInterceptor().apply { level = HttpLoggingInterceptor.Level.BODY }
        val client = OkHttpClient.Builder().addInterceptor(logging).build()

        return Retrofit.Builder()
            .baseUrl(if (baseUrl.endsWith('/')) baseUrl else "$baseUrl/")
            .client(client)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }
}

interface TopUpApi {
    @POST("accounts/top-up")
    suspend fun topUp(
        @Header("Authorization") auth: String,
        @Body request: TopUpRequest
    ): TopUpResponse
}

data class TopUpRequest(
    val amount: Double,
    val country: String,
    val appCountry: String
)

data class TopUpResponse(
    val success: Boolean,
    val country: String,
    val appCountry: String,
    val settlementCountry: String,
    val currency: String
)
