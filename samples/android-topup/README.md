# Android Studio Top-up Sample (LA via TH)

ตัวอย่างโปรเจกต์ Android Studio สำหรับเรียก API `POST /accounts/top-up` ที่ฝั่งเซิร์ฟเวอร์รองรับเงื่อนไข:
- `country = "LA"`
- `appCountry = "TH"`

## วิธีใช้งาน
1. เปิดโฟลเดอร์ `samples/android-topup` ด้วย Android Studio
2. รอ Gradle sync
3. รันแอปบน emulator/device
4. กรอกค่า:
   - Base URL ของ API (ต้องลงท้ายด้วย `/` หรือระบบจะเติมให้)
   - Bearer token
   - Amount
5. กดปุ่ม **Top up (LA via TH)**

## โครงสร้างสำคัญ
- `app/src/main/java/com/example/topup/MainActivity.kt` : หน้า UI + เรียก Retrofit
- `TopUpRequest` : payload `{ amount, country, appCountry }`
- `TopUpResponse` : อ่านผล `{ success, country, appCountry, settlementCountry, currency }`

> หมายเหตุ: เป็น sample client สำหรับ Android Studio เพื่อเชื่อมกับ backend เท่านั้น
