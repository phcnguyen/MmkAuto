﻿using Auto.Common.Models.Payments;
using System;
using System.ComponentModel.DataAnnotations;

namespace Auto.Common.Models.Transactions;

/// <summary>
/// Đại diện cho một giao dịch tài chính, bao gồm các thông tin về số tiền, phương thức thanh toán và trạng thái.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Mã giao dịch duy nhất trong hệ thống.
    /// </summary>
    public int TransactionId { get; set; }

    /// <summary>
    /// Mã hóa đơn liên quan đến giao dịch (nếu có)
    /// - Null nếu không liên kết với hóa đơn cụ thể.
    /// </summary>
    public int? InvoiceId { get; set; }

    /// <summary>
    /// Loại giao dịch
    /// - <see cref="TransactionType.Revenue"/>: Giao dịch thu tiền
    /// - <see cref="TransactionType.Expense"/>: Giao dịch chi tiền
    /// - <see cref="TransactionType.DebtPayment"/>: Giao dịch trả nợ
    /// - <see cref="TransactionType.RepairCost"/>: Chi phí sửa chữa.
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Phương thức thanh toán của giao dịch
    /// - Ví dụ: Tiền mặt, chuyển khoản, thẻ tín dụng, ví điện tử.
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Số tiền liên quan đến giao dịch.
    /// - Giá trị phải lớn hơn 0.
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Transaction amount must be greater than 0.")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Trạng thái của giao dịch.
    /// - <see cref="TransactionStatus.Pending"/>: Đang chờ xử lý
    /// - <see cref="TransactionStatus.Completed"/>: Đã hoàn thành
    /// - <see cref="TransactionStatus.Failed"/>: Thất bại.
    /// </summary>
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    /// <summary>
    /// Mô tả chi tiết về giao dịch (tùy chọn)
    /// - Không được vượt quá 255 ký tự.
    /// </summary>
    [StringLength(255, ErrorMessage = "Description must not exceed 255 characters.")]
    public string Description { get; set; }

    /// <summary>
    /// Ngày thực hiện giao dịch
    /// - Mặc định là thời điểm tạo giao dịch.
    /// </summary>
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Người đã tạo giao dịch trong hệ thống
    /// - Giới hạn tối đa 50 ký tự
    /// - Có thể là tên người dùng hoặc mã nhân viên.
    /// </summary>
    [StringLength(50)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// Người gần nhất chỉnh sửa giao dịch
    /// - Giới hạn tối đa 50 ký tự
    /// - Null nếu chưa có chỉnh sửa nào.
    /// </summary>
    [StringLength(50)]
    public string ModifiedBy { get; set; }
}