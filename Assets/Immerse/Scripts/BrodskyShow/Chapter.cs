using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Immerse.Brodsky.Data;
using UnityEngine;

namespace Immerse.Brodsky
{
    public class Chapter
    {
        public int SortingOrder { get; }
        public string Name { get; }

        public float Duration => audio.length;

        public float StartTime { get; set; }
        public bool HasAR { get; }
        public bool IsRunningAR { get; set; }

        public string ArDurationHumanTime { get; }
        private float? _arDuration;
        public float ArDuration => _arDuration ??= ArDurationHumanTime.ToSeconds();


        public AudioClip audio;
        public AudioClip audioAR;
        public GameObject ar;
        public readonly List<Comment> comments;

        private CancellationTokenSource _tokenSource;
        public readonly float ArAvailableAt;
        

        public Chapter(ChapterData data)
        {
            SortingOrder = data.sortingOrder;
            Name = data.name;
            HasAR = data.hasAR;
            if (HasAR)
            {
                ArDurationHumanTime = data.arDuration;
                ArAvailableAt = data.arAvailableAt.ToSeconds();
            }

            comments = data.comments?.Select(x => new Comment(x)).ToList();
        }

        public void Exit()
        {
            IsRunningAR = false;
            Cancel();
        }

        public float GetCurrentTime()
        {
            return IsRunningAR ? Duration : BrodskyAudioPlayer.Instance.CurrentTime;
        }

        private void Cancel()
        {
            if (_tokenSource == null || _tokenSource.IsCancellationRequested)
            {
                return;
            }

            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }

        public async Task Run(Action<Comment> commentCallback, Action onCommentExit, Action arAvailableCallback)
        {
            _tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _tokenSource.Token;
            
            try
            {
                Task waitForAvailabilityTask = WaitForArAvailability(arAvailableCallback, cancellationToken);
                Task iterateThroughCommentsTask = IterateThroughComments(commentCallback, onCommentExit, cancellationToken);

                await Task.WhenAll(waitForAvailabilityTask, iterateThroughCommentsTask);
            }
            finally
            {
                Cancel();
            }
        }

        private async Task IterateThroughComments(Action<Comment> onCommentEnter, Action onCommentExit, CancellationToken cancellationToken)
        {
            onCommentExit?.Invoke();
            if (comments == null || comments.Count == 0)
            {
                return;
            }
            
            cancellationToken.ThrowIfCancellationRequested();

            float lastTimeCode = StartTime;
            var commentsToShow = comments.Where(x => x.ExitTime > StartTime).OrderBy(x => x.EnterTime);
            
            foreach (Comment comment in commentsToShow)
            {
                float waitForCommentEnter = comment.EnterTime - lastTimeCode;
                if (waitForCommentEnter > 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(waitForCommentEnter), cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        onCommentExit?.Invoke();
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
                onCommentEnter?.Invoke(comment);

                
                float waitForCommentExit = comment.ExitTime - comment.EnterTime;
                
                await Task.Delay(TimeSpan.FromSeconds(waitForCommentExit), cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    onCommentExit?.Invoke();
                    cancellationToken.ThrowIfCancellationRequested();
                }
                onCommentExit?.Invoke();
                
                lastTimeCode = comment.ExitTime;
            }
        }

        private async Task WaitForArAvailability(Action callback, CancellationToken cancellationToken)
        {
            if (!HasAR)
            {
                return;
            }
            
            cancellationToken.ThrowIfCancellationRequested();

            float waitForAR = ArAvailableAt - StartTime;
            if (waitForAR <= 0)
            {
                callback?.Invoke();
                return;
            }
            
            await Task.Delay(TimeSpan.FromSeconds(waitForAR), cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            callback?.Invoke();
        }
    }
}