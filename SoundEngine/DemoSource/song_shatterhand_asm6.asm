;this file for FamiTone2 library generated by FamiStudio

shatterhand_music_data:
	db 1
	dw @instruments
	dw @samples-3
	dw @song0ch0,@song0ch1,@song0ch2,@song0ch3,@song0ch4
	db <(@tempo_env9), >(@tempo_env9), 0, 0

@instruments:
	dw @env3,@env0,@env0,@env9
	dw @env7,@env1,@env0,@env9
	dw @env11,@env10,@env0,@env9
	dw @env13,@env8,@env0,@env9
	dw @env2,@env0,@env0,@env6
	dw @env2,@env0,@env5,@env6
	dw @env2,@env0,@env4,@env6
	dw @env12,@env10,@env0,@env9

@samples:
@env0:
	db $c0,$7f,$00,$00
@env1:
	db $c0,$bf,$c1,$00,$02
@env2:
	db $06,$c8,$c9,$c5,$00,$03,$c4,$c4,$c2,$00,$08
@env3:
	db $00,$cf,$7d,$cf,$00,$03
@env4:
	db $c2,$00,$00
@env5:
	db $c1,$00,$00
@env6:
	db $00,$c0,$08,$c0,$04,$bd,$03,$bd,$00,$03
@env7:
	db $00,$cf,$ca,$c3,$c2,$c0,$00,$05
@env8:
	db $c0,$c2,$c5,$00,$02
@env9:
	db $00,$c0,$7f,$00,$01
@env10:
	db $c0,$c1,$c2,$00,$02
@env11:
	db $00,$cb,$ca,$09,$c9,$00,$04
@env12:
	db $00,$ca,$c6,$c3,$c0,$00,$04
@env13:
	db $00,$cb,$cb,$c5,$03,$c4,$03,$c3,$03,$c2,$00,$09
@tempo_env9:
	db $01,$08,$03,$04,$80

@song0ch0:
@song0ch0loop:
@ref0:
	db $7e,$88,$22,$9b,$f9,$83,$25,$89,$f9,$81,$62,$22,$22,$9b,$f9,$83,$27,$89,$f9,$81,$62,$22,$22,$9b,$f9,$83
@ref1:
	db $2a,$89,$f9,$81,$62,$22,$22,$9b,$f9,$83,$29,$9b,$f9,$83,$27,$89,$f9,$81,$62,$29,$25,$89,$f9,$81,$62,$27,$20,$89,$f9,$81,$62,$25
@ref2:
	db $7e,$22,$9b,$f9,$83,$25,$89,$f9,$81,$62,$22,$22,$9b,$f9,$83,$27,$89,$f9,$81,$62,$22,$22,$9b,$f9,$83
	db $ff,$1c
	dw @ref1
@ref3:
	db $00,$62,$25,$8d,$33,$89,$f9,$81,$62,$20,$31,$89,$f9,$81,$62,$33,$2c,$89,$f9,$81,$62,$31,$33,$89,$f9,$81,$62,$2c,$31,$89,$f9,$81,$62,$33,$2c,$89,$f9,$81,$62,$31,$27,$89,$f9,$81,$62,$2c
@ref4:
	db $25,$89,$f9,$81,$62,$27,$20,$89,$f9,$81,$62,$25,$29,$89,$f9,$81,$62,$20,$25,$89,$f9,$81,$62,$29,$20,$89,$f9,$81,$62,$25,$1d,$89,$f9,$81,$62,$20,$20,$89,$f9,$81,$62,$1d,$22,$89,$f9,$81,$62,$20
@ref5:
	db $33,$89,$f9,$81,$62,$22,$00,$62,$22,$8d,$2a,$89,$f9,$81,$62,$33,$00,$62,$33,$8d,$62,$2a,$8f,$2c,$89,$f9,$81,$62,$2a,$00,$62,$2a,$8d,$62,$2c,$8f
@ref6:
	db $29,$89,$f9,$81,$62,$2c,$00,$62,$2c,$8d,$62,$29,$8f,$2a,$89,$f9,$81,$62,$29,$00,$62,$29,$8d,$27,$ad,$f9,$83
@ref7:
	db $29,$89,$f9,$81,$62,$27,$00,$62,$27,$8d,$25,$89,$f9,$81,$62,$29,$27,$d1,$f9,$83
@ref8:
	db $00,$62,$27,$8d,$1d,$89,$f9,$81,$62,$27,$20,$89,$f9,$81,$62,$1d,$22,$89,$f9,$81,$62,$20,$27,$89,$f9,$81,$62,$22,$25,$89,$f9,$81,$62,$27,$20,$89,$f9,$81,$62,$25,$22,$89,$f9,$81,$62,$20
@ref9:
	db $00,$62,$20,$8d,$62,$22,$8f,$2a,$89,$f9,$81,$62,$22,$00,$62,$22,$8d,$62,$2a,$8f,$2c,$89,$f9,$81,$62,$2a,$00,$62,$2a,$8d,$62,$2c,$8f
	db $ff,$16
	dw @ref6
	db $ff,$11
	dw @ref7
	db $ff,$26
	dw @ref8
@ref10:
	db $00,$62,$20,$8d,$62,$22,$8f,$8a,$2f,$ad,$f9,$83,$31,$89,$f9,$81,$62,$2f,$33,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$33
@ref11:
	db $36,$9b,$f9,$83,$38,$89,$f9,$81,$62,$36,$36,$89,$f9,$81,$62,$38,$00,$62,$38,$8d,$32,$ad,$f9,$83
@ref12:
	db $2f,$bf,$f9,$83,$35,$8f,$f9,$81,$62,$2f,$33,$8f,$f9,$81,$62,$35,$32,$8f,$f9,$81,$62,$33
@ref13:
	db $33,$9b,$f9,$83,$2e,$89,$f9,$81,$62,$33,$36,$9b,$f9,$83,$35,$89,$f9,$81,$62,$36,$33,$89,$f9,$81,$62,$35,$31,$89,$f9,$81,$62,$33
@ref14:
	db $00,$62,$33,$8d,$62,$31,$8f,$2f,$ad,$f9,$83,$31,$89,$f9,$81,$62,$2f,$33,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$33
@ref15:
	db $31,$9b,$f9,$83,$33,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$33,$00,$62,$33,$8d,$36,$ad,$f9,$83
@ref16:
	db $38,$bf,$f9,$83,$38,$89,$f9,$83,$3a,$89,$f9,$81,$62,$38,$3d,$89,$f9,$81,$62,$3a,$38,$89,$f9,$81,$62,$3d
@ref17:
	db $00,$62,$3d,$8d,$88,$2a,$89,$f9,$81,$62,$38,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$1e,$89,$f9,$81,$62,$25,$1d,$89,$f9,$81,$62,$1e,$20,$89,$f9,$81,$62,$1d,$22,$89,$f9,$81,$62,$20
	db $ff,$1c
	dw @ref5
	db $ff,$16
	dw @ref6
	db $ff,$11
	dw @ref7
	db $ff,$26
	dw @ref8
	db $ff,$19
	dw @ref9
	db $ff,$16
	dw @ref6
	db $ff,$11
	dw @ref7
	db $ff,$26
	dw @ref8
@ref18:
	db $00,$62,$20,$8d,$62,$22,$8f,$31,$9b,$f9,$83,$31,$89,$f9,$83,$33,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$33,$35,$8f
@ref19:
	db $9d,$f9,$83,$35,$89,$f9,$83,$33,$89,$f9,$81,$62,$35,$00,$62,$35,$8d,$32,$ad,$f9,$83
@ref20:
	db $33,$bf,$f9,$83,$33,$89,$f9,$83,$35,$89,$f9,$81,$62,$33,$36,$89,$f9,$81,$62,$35,$38,$8f
@ref21:
	db $9d,$f9,$83,$36,$89,$f9,$81,$62,$38,$35,$89,$f9,$81,$62,$36,$00,$62,$36,$8d,$33,$ad,$f9,$83
@ref22:
	db $00,$62,$33,$9f,$35,$9b,$f9,$83,$35,$89,$f9,$83,$36,$89,$f9,$81,$62,$35,$38,$89,$f9,$81,$62,$36,$3a,$8f
@ref23:
	db $9d,$f9,$83,$38,$89,$f9,$81,$62,$3a,$36,$89,$f9,$81,$62,$38,$00,$62,$38,$8d,$38,$89,$f9,$81,$62,$36,$36,$89,$f9,$81,$62,$38,$35,$89,$f9,$81,$62,$36
@ref24:
	db $32,$bf,$f9,$83,$33,$bf,$f9,$83
@ref25:
	db $35,$89,$f9,$81,$62,$33,$35,$8b,$f9,$81,$00,$62,$35,$8d,$35,$d1,$f9,$83
@ref26:
	db $3a,$89,$f9,$81,$62,$35,$33,$89,$f9,$81,$62,$3a,$2e,$89,$f9,$81,$62,$33,$38,$89,$f9,$81,$62,$2e,$31,$89,$f9,$81,$62,$38,$2c,$89,$f9,$81,$62,$31,$2a,$9b,$f9,$83
@ref27:
	db $00,$62,$2a,$8d,$2a,$89,$f9,$83,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$2a,$89,$f9,$81,$62,$25,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$27,$89,$f9,$81,$62,$25
@ref28:
	db $2a,$89,$f9,$81,$62,$27,$2a,$89,$f9,$83,$00,$62,$2a,$8d,$2c,$89,$f9,$81,$62,$2a,$2c,$89,$f9,$83,$00,$62,$2c,$8d,$2a,$89,$f9,$81,$62,$2c,$2a,$89,$f9,$83
@ref29:
	db $00,$62,$2a,$8d,$2c,$89,$2c,$81,$62,$2a,$2c,$89,$f9,$83,$00,$62,$2c,$8d,$33,$89,$f9,$81,$62,$2c,$31,$89,$f9,$81,$62,$33,$2f,$89,$f9,$81,$62,$31,$2e,$89,$f9,$81,$62,$2f
@ref30:
	db $3a,$89,$f9,$81,$62,$2e,$33,$89,$f9,$81,$62,$3a,$2e,$89,$f9,$81,$62,$33,$38,$89,$f9,$81,$62,$2e,$31,$89,$f9,$81,$62,$38,$2c,$89,$f9,$81,$62,$31,$2a,$9b,$f9,$83
	db $ff,$25
	dw @ref27
	db $ff,$21
	dw @ref28
	db $ff,$23
	dw @ref29
@ref31:
	db $23,$9b,$f9,$83,$2a,$89,$f9,$81,$62,$23,$25,$9b,$f9,$83,$2c,$9b,$f9,$83,$27,$8f
@ref32:
	db $8b,$f9,$83,$2e,$9b,$f9,$83,$35,$89,$f9,$81,$62,$2e,$36,$89,$f9,$81,$35,$8b,$f9,$81,$62,$36,$33,$89,$f9,$81,$62,$35,$31,$89,$f9,$81,$62,$33
@ref33:
	db $36,$89,$f9,$81,$62,$31,$36,$89,$f9,$83,$00,$62,$36,$8d,$35,$89,$f9,$81,$62,$36,$35,$89,$f9,$83,$00,$62,$35,$8d,$36,$89,$f9,$81,$62,$35,$36,$89,$f9,$83
@ref34:
	db $00,$62,$36,$8d,$1d,$89,$f9,$81,$62,$36,$22,$89,$f9,$81,$62,$1d,$29,$89,$f9,$81,$62,$22,$31,$89,$f9,$81,$62,$29,$35,$ad,$f9,$83
	db $fd
	dw @song0ch0loop

@song0ch1:
@song0ch1loop:
@ref35:
	db $8a,$27,$9b,$f9,$83,$2a,$89,$f9,$81,$62,$27,$27,$9b,$f9,$83,$2c,$89,$f9,$81,$62,$27,$27,$9b,$f9,$83
@ref36:
	db $2e,$89,$f9,$81,$62,$27,$27,$9b,$f9,$83,$2c,$9b,$f9,$83,$2a,$89,$f9,$81,$62,$2c,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29
@ref37:
	db $27,$9b,$f9,$83,$2a,$89,$f9,$81,$62,$27,$27,$9b,$f9,$83,$2c,$89,$f9,$81,$62,$27,$27,$9b,$f9,$83
	db $ff,$1c
	dw @ref36
@ref38:
	db $00,$62,$29,$8d,$36,$89,$f9,$81,$62,$25,$35,$89,$f9,$81,$62,$36,$31,$89,$f9,$81,$62,$35,$36,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$36,$31,$89,$f9,$81,$62,$35,$2a,$89,$f9,$81,$62,$31
@ref39:
	db $29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$2c,$89,$f9,$81,$62,$25,$29,$89,$f9,$81,$62,$2c,$25,$89,$f9,$81,$62,$29,$22,$89,$f9,$81,$62,$25,$25,$89,$f9,$81,$62,$22,$27,$89,$f9,$81,$62,$25
@ref40:
	db $3f,$89,$f9,$81,$62,$27,$00,$62,$27,$8d,$2e,$89,$f9,$81,$62,$3f,$00,$62,$3f,$8d,$62,$2e,$8f,$2f,$89,$f9,$81,$62,$2e,$00,$62,$2e,$8d,$62,$2f,$8f
@ref41:
	db $2c,$89,$f9,$81,$62,$2f,$00,$62,$2f,$8d,$62,$2c,$8f,$2e,$89,$f9,$81,$62,$2c,$00,$62,$2c,$8d,$2a,$ad,$f9,$83
@ref42:
	db $2c,$89,$f9,$81,$62,$2a,$00,$62,$2a,$8d,$29,$89,$f9,$81,$62,$2c,$2a,$d1,$f9,$83
@ref43:
	db $00,$62,$2a,$8d,$22,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$22,$27,$89,$f9,$81,$62,$25,$2a,$89,$f9,$81,$62,$27,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$27,$89,$f9,$81,$62,$25
@ref44:
	db $00,$62,$25,$8d,$62,$27,$8f,$2e,$89,$f9,$81,$62,$27,$00,$62,$27,$8d,$62,$2e,$8f,$2f,$89,$f9,$81,$62,$2e,$00,$62,$2e,$8d,$62,$2f,$8f
	db $ff,$16
	dw @ref41
	db $ff,$11
	dw @ref42
	db $ff,$26
	dw @ref43
@ref45:
	db $00,$62,$25,$8d,$62,$27,$8f,$8c,$33,$ad,$f9,$83,$35,$89,$f9,$81,$62,$33,$36,$89,$f9,$81,$62,$35,$38,$89,$f9,$81,$62,$36
@ref46:
	db $3a,$9b,$f9,$83,$3b,$89,$f9,$81,$62,$3a,$3a,$89,$f9,$81,$62,$3b,$00,$62,$3b,$8d,$35,$ad,$f9,$83
@ref47:
	db $38,$bf,$f9,$83,$38,$8f,$f9,$83,$36,$8f,$f9,$81,$62,$38,$35,$8f,$f9,$81,$62,$36
@ref48:
	db $36,$9b,$f9,$83,$33,$89,$f9,$81,$62,$36,$3a,$d1,$f9,$83
@ref49:
	db $00,$62,$3a,$9f,$33,$ad,$f9,$83,$35,$89,$f9,$81,$62,$33,$36,$89,$f9,$81,$62,$35,$38,$89,$f9,$81,$62,$36
@ref50:
	db $3a,$9b,$f9,$83,$3b,$89,$f9,$81,$62,$3a,$3d,$89,$f9,$81,$62,$3b,$00,$62,$3b,$8d,$3f,$ad,$f9,$83
@ref51:
	db $41,$bf,$f9,$83,$41,$89,$f9,$83,$42,$89,$f9,$81,$62,$41,$44,$89,$f9,$81,$62,$42,$41,$89,$f9,$81,$62,$44
@ref52:
	db $00,$62,$44,$8d,$8a,$36,$89,$f9,$81,$62,$41,$35,$89,$f9,$81,$62,$36,$31,$89,$f9,$81,$62,$35,$2a,$89,$f9,$81,$62,$31,$29,$89,$f9,$81,$62,$2a,$25,$89,$f9,$81,$62,$29,$27,$89,$f9,$81,$62,$25
	db $ff,$1c
	dw @ref40
	db $ff,$16
	dw @ref41
	db $ff,$11
	dw @ref42
	db $ff,$26
	dw @ref43
	db $ff,$19
	dw @ref44
	db $ff,$16
	dw @ref41
	db $ff,$11
	dw @ref42
	db $ff,$26
	dw @ref43
@ref53:
	db $00,$62,$25,$8d,$62,$27,$8f,$35,$9b,$f9,$83,$35,$89,$f9,$83,$36,$89,$f9,$81,$62,$35,$38,$89,$f9,$81,$62,$36,$3a,$8f
@ref54:
	db $9d,$f9,$83,$38,$89,$f9,$81,$62,$3a,$36,$89,$f9,$81,$62,$38,$00,$62,$38,$8d,$35,$ad,$f9,$83
@ref55:
	db $36,$bf,$f9,$83,$36,$8b,$f9,$81,$38,$89,$f9,$81,$62,$36,$3a,$89,$f9,$81,$62,$38,$3b,$8f
@ref56:
	db $9d,$f9,$83,$3a,$89,$f9,$81,$62,$3b,$38,$89,$f9,$81,$62,$3a,$00,$62,$3a,$8d,$36,$ad,$f9,$83
@ref57:
	db $00,$62,$36,$9f,$38,$9b,$f9,$83,$38,$89,$f9,$83,$3a,$89,$f9,$81,$62,$38,$3b,$89,$f9,$81,$62,$3a,$3d,$8f
@ref58:
	db $9d,$f9,$83,$3b,$89,$f9,$81,$62,$3d,$3a,$89,$f9,$81,$62,$3b,$00,$62,$3b,$8d,$3b,$89,$f9,$81,$62,$3a,$3a,$89,$f9,$81,$62,$3b,$38,$89,$f9,$81,$62,$3a
@ref59:
	db $3a,$bf,$f9,$83,$3c,$bf,$f9,$83
@ref60:
	db $3d,$89,$f9,$81,$62,$3c,$3d,$89,$f9,$83,$00,$62,$3d,$8d,$3e,$d1,$f9,$83
@ref61:
	db $3f,$89,$f9,$81,$62,$3e,$3a,$89,$f9,$81,$62,$3f,$33,$89,$f9,$81,$62,$3a,$3d,$89,$f9,$81,$62,$33,$38,$89,$f9,$81,$62,$3d,$31,$89,$f9,$81,$62,$38,$2f,$9b,$f9,$83
@ref62:
	db $00,$62,$2f,$8d,$36,$89,$f9,$81,$62,$2f,$35,$89,$f9,$81,$62,$36,$31,$89,$f9,$81,$62,$35,$36,$89,$f9,$81,$62,$31,$35,$89,$f9,$81,$62,$36,$31,$89,$f9,$81,$62,$35,$33,$89,$f9,$81,$62,$31
@ref63:
	db $2f,$89,$f9,$81,$62,$33,$2f,$89,$f9,$83,$00,$62,$2f,$8d,$31,$89,$f9,$81,$62,$2f,$31,$89,$f9,$83,$00,$62,$31,$8d,$33,$89,$f9,$81,$62,$31,$33,$89,$f9,$83
@ref64:
	db $00,$62,$33,$8d,$35,$89,$f9,$81,$62,$33,$35,$89,$f9,$83,$00,$62,$35,$8d,$36,$89,$f9,$81,$62,$35,$35,$89,$f9,$81,$62,$36,$33,$89,$f9,$81,$62,$35,$31,$89,$f9,$81,$62,$33
@ref65:
	db $3f,$89,$f9,$81,$62,$31,$3a,$89,$f9,$81,$62,$3f,$33,$89,$f9,$81,$62,$3a,$3d,$89,$f9,$81,$62,$33,$38,$89,$f9,$81,$62,$3d,$31,$89,$f9,$81,$62,$38,$2f,$9b,$f9,$83
	db $ff,$26
	dw @ref62
	db $ff,$21
	dw @ref63
	db $ff,$23
	dw @ref64
@ref66:
	db $2f,$9b,$f9,$83,$36,$89,$f9,$81,$62,$2f,$31,$9b,$f9,$83,$38,$9b,$f9,$83,$33,$8f
@ref67:
	db $8b,$f9,$83,$3a,$9b,$f9,$83,$41,$89,$f9,$81,$62,$3a,$42,$89,$f9,$81,$62,$41,$41,$89,$f9,$81,$62,$42,$3f,$89,$f9,$81,$62,$41,$3d,$89,$f9,$81,$62,$3f
@ref68:
	db $3f,$89,$f9,$81,$62,$3d,$3f,$89,$f9,$83,$00,$62,$3f,$8d,$3d,$89,$f9,$81,$62,$3f,$3d,$89,$f9,$83,$00,$62,$3d,$8d,$3f,$89,$f9,$81,$62,$3d,$3f,$89,$f9,$83
@ref69:
	db $00,$62,$3f,$8d,$22,$89,$f9,$81,$62,$3f,$29,$89,$f9,$81,$62,$22,$2e,$89,$f9,$81,$62,$29,$35,$89,$f9,$81,$62,$2e,$3a,$ad,$f9,$83
	db $fd
	dw @song0ch1loop

@song0ch2:
@song0ch2loop:
@ref70:
	db $80,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81
@ref71:
	db $27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$25,$9d,$00,$81,$22,$8b,$00,$81,$25,$8b,$00,$81,$29,$8b,$00,$81
@ref72:
	db $27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81
	db $ff,$1c
	dw @ref71
@ref73:
	db $25,$f7,$00,$81,$25,$8f
@ref74:
	db $c3,$00,$81,$25,$8b,$00,$81,$22,$8b,$00,$81,$25,$8b,$00,$81,$27,$8b,$00,$81
	db $ff,$20
	dw @ref72
	db $ff,$20
	dw @ref72
	db $ff,$20
	dw @ref72
@ref75:
	db $27,$8b,$00,$81,$22,$8b,$00,$81,$25,$8b,$00,$81,$27,$8b,$00,$81,$2a,$8b,$00,$81,$29,$8b,$00,$81,$25,$8b,$00,$81,$27,$8b,$00,$81
@ref76:
	db $23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81
	db $ff,$20
	dw @ref76
	db $ff,$20
	dw @ref76
@ref77:
	db $23,$8b,$00,$81,$22,$8b,$00,$81,$25,$8b,$00,$81,$27,$8b,$00,$81,$2a,$8b,$00,$81,$29,$8b,$00,$81,$25,$8b,$00,$81,$27,$8b,$00,$81
@ref78:
	db $20,$8b,$00,$81,$20,$8b,$00,$81,$27,$8b,$00,$81,$2a,$8b,$00,$81,$20,$8b,$00,$81,$20,$8b,$00,$81,$27,$8b,$00,$81,$2a,$8b,$00,$81
@ref79:
	db $22,$8b,$00,$81,$22,$8b,$00,$81,$29,$8b,$00,$81,$2c,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$29,$8b,$00,$81,$2c,$8b,$00,$81
@ref80:
	db $20,$8b,$00,$81,$20,$8b,$00,$81,$27,$8b,$00,$81,$2a,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$29,$8b,$00,$81,$2c,$8b,$00,$81
@ref81:
	db $27,$8b,$00,$81,$27,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81
	db $ff,$20
	dw @ref78
	db $ff,$20
	dw @ref79
@ref82:
	db $23,$8b,$00,$81,$23,$8b,$00,$81,$2a,$8b,$00,$81,$2f,$8b,$00,$81,$23,$8b,$00,$81,$2a,$8b,$00,$81,$2f,$8b,$00,$81,$25,$8b,$00,$81
@ref83:
	db $91,$25,$8b,$00,$81,$25,$9d,$00,$81,$25,$8b,$00,$81,$25,$9d,$00,$81,$27,$8b,$00,$81
	db $ff,$20
	dw @ref72
	db $ff,$20
	dw @ref72
	db $ff,$20
	dw @ref72
	db $ff,$20
	dw @ref75
	db $ff,$20
	dw @ref76
	db $ff,$20
	dw @ref76
	db $ff,$20
	dw @ref76
	db $ff,$20
	dw @ref77
@ref84:
	db $25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81
@ref85:
	db $26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81,$26,$8b,$00,$81
	db $ff,$20
	dw @ref72
@ref86:
	db $28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81,$28,$8b,$00,$81
@ref87:
	db $29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81,$29,$8b,$00,$81
	db $ff,$20
	dw @ref84
@ref88:
	db $22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81
@ref89:
	db $22,$8b,$00,$81,$22,$8b,$00,$81,$29,$8b,$00,$81,$2e,$9d,$00,$81,$2e,$8b,$00,$81,$29,$8b,$00,$81,$22,$8b,$00,$81
@ref90:
	db $27,$8b,$00,$81,$27,$8b,$00,$81,$27,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$23,$a1
@ref91:
	db $8d,$00,$81,$23,$8b,$00,$81,$23,$9d,$00,$81,$23,$8b,$00,$81,$23,$9d,$00,$81,$23,$8b,$00,$81
@ref92:
	db $20,$8b,$00,$81,$20,$8b,$00,$81,$20,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81
@ref93:
	db $23,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81
	db $ff,$1a
	dw @ref90
	db $ff,$17
	dw @ref91
	db $ff,$20
	dw @ref92
	db $ff,$20
	dw @ref93
@ref94:
	db $20,$8b,$00,$81,$20,$8b,$00,$81,$20,$8b,$00,$81,$20,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81
@ref95:
	db $23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$23,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81,$25,$8b,$00,$81
@ref96:
	db $27,$8b,$00,$81,$27,$8b,$00,$93,$25,$8b,$00,$81,$25,$8b,$00,$93,$27,$8b,$00,$81,$27,$8b,$00,$81
@ref97:
	db $91,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$22,$8b,$00,$81,$24,$8b,$00,$81,$26,$8b,$00,$81
	db $fd
	dw @song0ch2loop

@song0ch3:
@song0ch3loop:
@ref98:
	db $84,$2d,$a1,$86,$27,$a1,$84,$2d,$a1,$86,$27,$a1
	db $ff,$08
	dw @ref98
	db $ff,$08
	dw @ref98
	db $ff,$08
	dw @ref98
@ref99:
	db $84,$2d,$a1,$2d,$a1,$2d,$a1,$2d,$a1
@ref100:
	db $2d,$a1,$2d,$a1,$86,$27,$8f,$84,$2d,$8f,$86,$27,$81,$27,$8b,$84,$2d,$8f
@ref101:
	db $82,$21,$8f,$8e,$2d,$8f,$86,$27,$8f,$8e,$2d,$8f,$82,$21,$8f,$21,$8f,$86,$27,$8f,$8e,$2d,$8f
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
@ref102:
	db $82,$21,$8f,$86,$27,$8f,$27,$8f,$82,$21,$8f,$86,$27,$8f,$27,$8f,$82,$21,$8f,$86,$27,$8f
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref102
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
@ref103:
	db $86,$27,$8f,$82,$21,$8f,$21,$8f,$86,$27,$8f,$82,$21,$8f,$86,$27,$8f,$27,$8f,$27,$8f
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref102
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref102
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
	db $ff,$10
	dw @ref101
@ref104:
	db $86,$27,$8f,$27,$8f,$82,$21,$8f,$86,$27,$8f,$82,$21,$8f,$86,$27,$8f,$27,$8f,$27,$8f
@ref105:
	db $27,$8f,$27,$a1,$27,$8f,$27,$a1,$27,$8f,$27,$8f
@ref106:
	db $91,$82,$21,$8f,$86,$27,$8f,$82,$21,$8f,$86,$27,$8f,$82,$21,$8f,$86,$27,$8f,$27,$8f
@ref107:
	db $82,$21,$8f,$21,$8f,$84,$2d,$8f,$82,$21,$8f,$21,$8f,$84,$2d,$8f,$82,$21,$8f,$21,$8f
@ref108:
	db $84,$2d,$8f,$82,$21,$8f,$21,$8f,$84,$2d,$8f,$86,$27,$a1,$27,$a1
	db $ff,$0c
	dw @ref105
	db $ff,$0f
	dw @ref106
	db $ff,$10
	dw @ref107
	db $ff,$0c
	dw @ref108
	db $ff,$08
	dw @ref98
	db $ff,$08
	dw @ref98
	db $ff,$0c
	dw @ref105
@ref109:
	db $91,$27,$8f,$27,$8f,$27,$8f,$27,$8f,$27,$8f,$82,$21,$8f,$21,$8f
	db $fd
	dw @song0ch3loop

@song0ch4:
@song0ch4loop:
@ref110:
	db $f7,$97
@ref111:
	db $f7,$97
@ref112:
	db $f7,$97
@ref113:
	db $f7,$97
@ref114:
	db $f7,$97
@ref115:
	db $f7,$97
@ref116:
	db $f7,$97
@ref117:
	db $f7,$97
@ref118:
	db $f7,$97
@ref119:
	db $f7,$97
@ref120:
	db $f7,$97
@ref121:
	db $f7,$97
@ref122:
	db $f7,$97
@ref123:
	db $f7,$97
@ref124:
	db $f7,$97
@ref125:
	db $f7,$97
@ref126:
	db $f7,$97
@ref127:
	db $f7,$97
@ref128:
	db $f7,$97
@ref129:
	db $f7,$97
@ref130:
	db $f7,$97
@ref131:
	db $f7,$97
@ref132:
	db $f7,$97
@ref133:
	db $f7,$97
@ref134:
	db $f7,$97
@ref135:
	db $f7,$97
@ref136:
	db $f7,$97
@ref137:
	db $f7,$97
@ref138:
	db $f7,$97
@ref139:
	db $f7,$97
@ref140:
	db $f7,$97
@ref141:
	db $f7,$97
@ref142:
	db $f7,$97
@ref143:
	db $f7,$97
@ref144:
	db $f7,$97
@ref145:
	db $f7,$97
@ref146:
	db $f7,$97
@ref147:
	db $f7,$97
@ref148:
	db $f7,$97
@ref149:
	db $f7,$97
@ref150:
	db $f7,$97
@ref151:
	db $f7,$97
@ref152:
	db $f7,$97
@ref153:
	db $f7,$97
@ref154:
	db $f7,$97
@ref155:
	db $f7,$97
@ref156:
	db $f7,$97
@ref157:
	db $f7,$97
@ref158:
	db $f7,$97
@ref159:
	db $f7,$97
	db $fd
	dw @song0ch4loop
