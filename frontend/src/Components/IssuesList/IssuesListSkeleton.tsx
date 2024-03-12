const IssuesListSkeleton = () => {
  return (
    <div className="flex flex-col gap-4 w-52">
      <div className="skeleton h-6 w-80"></div>
      <div className="skeleton h-4 w-96"></div>
      <div className="skeleton h-4 w-80"></div>
      <div className="skeleton h-4 w-96"></div>
    </div>
  );
};

export default IssuesListSkeleton;
