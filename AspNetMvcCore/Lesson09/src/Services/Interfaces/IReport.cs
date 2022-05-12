using Domain.Entities;

namespace Interfaces;

public interface IReport
{
    string Create(Report reportData);
}
