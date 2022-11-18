document.addEventListener("DOMContentLoaded",function() {


	$('.MuiTenXuong').click(function(event) {
		$('.MuiTen').toggleClass('HienLen');
		$('.ThongBao').removeClass('HienLen');
	});

    $('.BaoXau').click(function (event) {
        $('.FormBaoXau').addClass('HienLen');
        $('.LamMo').addClass('HienLen');
	});

	$('.Chuong').click(function(event) {
		$('.ThongBao').toggleClass('HienLen');
		$('.SLTB').addClass('BienMat');
		$('.MuiTen').removeClass('HienLen');
	});

	$('.container').click(function(event) {
		
		$('.MuiTen').removeClass('HienLen');
		$('.ThongBao').removeClass('HienLen');
	});

	$('.DatMua').click(function(event) {
		$('.ThongTinMuaHang').addClass('HienLen');
		$('.LamMo').addClass('HienLen');
	});

	$('.LamMo,.submit').click(function(event) {
		$('.ThongTinMuaHang').removeClass('HienLen');
        $('.LamMo').removeClass('HienLen');
        $('.FormBaoXau').removeClass('HienLen');
        $('.ChonDiaDiem').removeClass('HienLen');
	});
    $('.DiaDiem').click(function (event) {
        $('.ChonDiaDiem').addClass('HienLen');
        $('.LamMo').addClass('HienLen');
    });
},false)
