// Fast ADC reading code by "jmknapp" from Arduino forum
// http://forum.arduino.cc/index.php?topic=6549.0
// Increase frequency of ADC readings
#define cbi(sfr, bit) (_SFR_BYTE(sfr) &= ~_BV(bit))
#define sbi(sfr, bit) (_SFR_BYTE(sfr) |= _BV(bit))

void InitFastADC() {
    // set ADC prescale to 16 to speedup readings
    sbi(ADCSRA,ADPS2);
    cbi(ADCSRA,ADPS1);
    cbi(ADCSRA,ADPS0);
}
