using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

/// <summary>
/// Unit of work.
/// </summary>
internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;

    /// <summary>
    /// Creates service.
    /// </summary>
    /// <param name="context">Context.</param>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Begins transaction.
    /// </summary>
    /// <param name="isolationLevel">Isolation level.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    )
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction is already started.");
        }

        _transaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    /// <summary>
    /// Commits transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit.");
        }

        await _context.SaveChangesAsync(cancellationToken);
        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Rollbacks transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback.");
        }

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Saves changes.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Disposes service.
    /// </summary>
    public void Dispose() => _transaction?.Dispose();

    /// <summary>
    /// Disposes service.
    /// </summary>
    public async ValueTask DisposeAsync() => await (_transaction?.DisposeAsync() ?? ValueTask.CompletedTask);
}