$(document).ready(function () {
    $(".Khoa").click(function () {
        MaVP = $(this).data('id');
        KhoaVatPham(MaVP);
    })
    $(".MoKhoa").click(function () {
        MaVP = $(this).data('id');
        MoKhoaVatPham(MaVP);
    })
    $(".Duyet").click(function () {
        MaVP = $(this).data('id');
        DuyetVatPham(MaVP);
    })
    $(".Xoa").click(function () {
        var r = confirm("Bấm OK để xóa");
        if (r == true) {
            MaVP = $(this).data('id');
            XoaVatPham(MaVP);

        }
    })
});
function KhoaVatPham(MaVP) {
    $.ajax({
        async: true,
        type: "POST",
        url: "/XuLyAdmin/KhoaVatPham",

        data: { MaVP: MaVP },
        success: function (response) {
            if (response.status) {
                alert("Khóa thành công!")
            } else {
                alert("Vật phẩm này hiện đang bị khóa!");
            }
        }
    });
}
function MoKhoaVatPham(MaVP) {
    $.ajax({
        
        type: "POST",
        url: "/XuLyAdmin/MoKhoaVatPham",
        data: { MaVP: MaVP },
        success: function (response) {
            if (response.status) {
                alert("Mở khóa thành công!");
                location.reload();
            } else {
                alert("Mở khóa thất bại!");
            }
        }
    });
}
function DuyetVatPham(MaVP) {
    $.ajax({
        
        type: "POST",
        url: "/XuLyAdmin/DuyetVatPham",
        data: { MaVP: MaVP },
        success: function (response) {
            if (response.status) {
                alert("Đã duyệt");
                location.reload();
            } else {
                alert("Duyệt vật phẩm thất bại");
            }
        }
    });
}
function XoaVatPham(MaVP) {
    $.ajax({
        
        type: "POST",
        url: "/XuLyAdmin/XoaVatPham",
        data: { MaVP: MaVP },
        success: function (response) {
            if (response.status) {
                alert("Xóa thành công!");
                location.reload();
            } else {
                alert("Xóa thất bại");
            }
        }
    });
}