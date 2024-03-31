import { useEffect, useState } from "react";
import BoardSkeleton from "./BoardSkeleton";

const Board = () => {
  const columns = 3;
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    setInterval(() => {
      setIsLoading(false);
    }, 1000);
  }, []);

  return (
    <>
      {isLoading ? (
        <BoardSkeleton />
      ) : (
        <div className="flex flex-wrap">
          {Array.from({ length: columns }, (_, index) => (
            <div key={index} className="bg-base-300 m-2 p-2 rounded-xl">
              <p className="font-bold text-xl text-center m-2">To Do</p>
              <div className="card w-80 bg-base-100 shadow-xl mb-2">
                <div className="card-body">
                  <h2 className="card-title">Card title!</h2>
                  <p>If a dog chews shoes whose shoes does he choose?</p>
                  <div className="card-actions justify-end">
                    <button className="btn btn-primary">Buy Now</button>
                  </div>
                </div>
              </div>
              <div className="card w-80 bg-base-100 shadow-xl">
                <div className="card-body">
                  <h2 className="card-title">Card title!</h2>
                  <p>If a dog chews shoes whose shoes does he choose?</p>
                  <div className="card-actions justify-end">
                    <button className="btn btn-primary">Buy Now</button>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </>
  );
};

export default Board;
